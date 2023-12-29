using AutoMapper;
using Bogus;
using Microsoft.Extensions.Logging;
using Moq;
using Notification.Api.External.Application.EventHandlers;
using Notification.Api.External.Application.Events;
using Notification.Api.External.Application.Events.v1;
using Notification.Api.External.Application.MapperResolvers;
using Notification.Api.External.Core.Contracts;
using Notification.Api.External.Core.Enums;
using Notification.Api.External.Core.Exceptions;
using Notification.Api.External.Data.Repositories;
using Notification.Api.External.Eventbus.Envelopes;
using Notification.Api.External.Eventbus.Publishers;
using System.Text.Json;

namespace Notification.Api.External.UnitTests.EventBus.EventHandlers;
public class NotificationDeliveryHandlerTests
{
    private static readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly Faker _faker;
    private readonly Mock<IMapper> _mapperMock;
    private readonly NotificationEvent _commandStub;
    private readonly Mock<IWriteRepository> _writeRepositoryMock;
    private readonly Mock<IEnvelopPublisher> _apiEventBusRepositoryMock;
    private readonly Mock<ILogger<NotificationEventHandler>> _loggerMock;
    private readonly NotificationEventHandler _notificationDeliveryHandler;
    private readonly Mock<IMapperResolver<NotificationEvent, IEvent>> _mapperResolverMock;

    public NotificationDeliveryHandlerTests()
    {
        _faker = new();
        _loggerMock = new();
        _mapperMock = new();
        _mapperResolverMock = new();
        _writeRepositoryMock = new();
        _apiEventBusRepositoryMock = new();
        _commandStub = new NotificationEvent(
            _faker.Person,
            _faker.Random.Guid().ToString(),
            EmailEventTypes.Leitura.ToString(),
            NotificationType.Email,
            ProvidersNotification.IAgenteSmtp);

        _notificationDeliveryHandler = new NotificationEventHandler(
            _mapperMock.Object,
            _writeRepositoryMock.Object,
            _apiEventBusRepositoryMock.Object,
            _loggerMock.Object,
            _mapperResolverMock.Object);
    }

    [Fact]
    public async Task Should_Throw_Exception_If_Request_EventBusApi_Fail()
    {
        var eventStub = new EmailClickEvent(_commandStub);
        _mapperResolverMock.Setup(x => x.Map(It.IsAny<NotificationEvent>())).Returns(eventStub);

        var envelopStub = new Envelope(
            eventStub.Id,
            eventStub.Version,
            eventStub.Type,
            eventStub.Source,
            eventStub.Description,
            eventStub.ContentType,
            eventStub.ToBase64Data(_options),
            DateTime.UtcNow,
            eventStub.Schema);

        _mapperMock.Setup(x => x.Map<Envelope>(It.IsAny<IEvent>())).Returns(envelopStub);

        _apiEventBusRepositoryMock
           .Setup(x => x.PublishAsync(It.IsAny<IEnvelope>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(new HttpResponseMessage()
           {
               StatusCode = System.Net.HttpStatusCode.InternalServerError
           });

        await Assert.ThrowsAsync<EnvelopPublishingException>(() =>
            _notificationDeliveryHandler.Handle(_commandStub, default));
    }

    [Fact]
    public async Task Should_Not_Throw_Exception_If_Request_EventBusApi_Is_Successful()
    {
        _apiEventBusRepositoryMock
           .Setup(x => x.PublishAsync(It.IsAny<IEnvelope>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(new HttpResponseMessage()
           {
               StatusCode = System.Net.HttpStatusCode.OK
           });

        await _notificationDeliveryHandler.Handle(_commandStub, default);
    }

    [Fact]
    public async Task Should_Invoke_Client_ApiEventBus()
    {
        _apiEventBusRepositoryMock
           .Setup(x => x.PublishAsync(It.IsAny<IEnvelope>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(new HttpResponseMessage()
           {
               StatusCode = System.Net.HttpStatusCode.OK
           });

        await _notificationDeliveryHandler.Handle(_commandStub, default);

        _apiEventBusRepositoryMock.Verify(
            x => x.PublishAsync(It.IsAny<IEnvelope>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Should_Use_Data_From_Command_In_Envelope()
    {
        IEnvelope envelopeStub = UnitTestsHelpers.MockAllMappingsInNotificationEventHandler(_mapperResolverMock, _mapperMock, _commandStub);

        _apiEventBusRepositoryMock
           .Setup(x => x.PublishAsync(It.IsAny<IEnvelope>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(new HttpResponseMessage()
           {
               StatusCode = System.Net.HttpStatusCode.OK
           });

        var cancellationToken = new CancellationToken();
        await _notificationDeliveryHandler.Handle(_commandStub, cancellationToken);

        _apiEventBusRepositoryMock.Verify(x => x.PublishAsync(envelopeStub, cancellationToken), Times.Once);
    }
}
