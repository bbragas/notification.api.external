using AutoMapper;
using Moq;
using Notification.Api.External.Application.Events.v1;
using Notification.Api.External.Application.Events;
using Notification.Api.External.Application.MapperResolvers;
using Notification.Api.External.Core.Contracts;
using Notification.Api.External.Core.Models;
using Notification.Api.External.Eventbus.Envelopes;
using System.Text.Json;

namespace Notification.Api.External.UnitTests;
public class UnitTestsHelpers
{
    private static readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static IEnvelope MockAllMappingsInNotificationEventHandler(
        Mock<IMapperResolver<NotificationEvent, IEvent>> mapperResolverMock,
        Mock<IMapper> mapperMock,
        NotificationEvent commandStub)
    {
        var eventStub = new EmailReadEvent(commandStub);
        mapperResolverMock.Setup(x => x.Map(It.IsAny<NotificationEvent>())).Returns(eventStub);

        mapperMock.Setup(x => x.Map<NotificationDelivery>(It.IsAny<NotificationEvent>()))
            .Returns(new NotificationDelivery
            {
                Content = commandStub.Content,
                EventType = commandStub.EventType,
                NotificationId = commandStub.NotificationId,
                NotificationType = commandStub.NotificationType,
                Provider = commandStub.Provider
            });

        var envelopeStub = new Envelope(
            eventStub.Id,
            eventStub.Version,
            eventStub.Type,
            eventStub.Source,
            eventStub.Description,
            eventStub.ContentType,
            eventStub.ToBase64Data(_options),
            DateTime.UtcNow,
            eventStub.Schema);
        mapperMock.Setup(x => x.Map<Envelope>(It.IsAny<IEvent>())).Returns(envelopeStub);

        return envelopeStub;
    }
}
