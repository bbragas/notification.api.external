using Bogus;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Notification.Api.External.Application.Events;
using Notification.Api.External.Core.Enums;
using Notification.Api.External.Webhook.Controllers.v1;
using Notification.Api.External.Webhook.Dtos;

namespace Notification.Api.External.UnitTests.Webhook.Controllers;
public class InfobipWebhookControllerTests
{
    private readonly Faker _faker;
    private readonly SmsDeliveryDto _dtoStub;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly InfobipWebhookController _infobipWebhookController;
    private readonly string _notificationId;
    private readonly Mock<ILogger<InfobipWebhookController>> _loggerMock;

    public InfobipWebhookControllerTests()
    {
        _faker = new();
        _loggerMock = new();
        _mediatorMock = new();
        _infobipWebhookController = new(_loggerMock.Object, _mediatorMock.Object);

        _notificationId = Guid.NewGuid().ToString();
        _dtoStub = new SmsDeliveryDto()
        {
            Results = new()
            {
                new()
                {
                    DoneAt = DateTime.UtcNow,
                    Error = new(),
                    MessageId = Guid.NewGuid().ToString(),
                    SentAt = DateTime.UtcNow,
                    Status = new()
                    {
                        GroupName = _faker.Random.Enum<SmsEventTypes>()
                    }
                }
            }
        };
    }

    [Fact]
    public async void Should_Call_LogInformation()
    {
        await _infobipWebhookController.Receive(_dtoStub, _notificationId);

        _loggerMock.Verify(
               x => x.Log(
                   It.Is<LogLevel>(l => l == LogLevel.Information),
                   It.IsAny<EventId>(),
                   It.Is<It.IsAnyType>((v, t) => true),
                   It.IsAny<Exception>(),
                   It.Is<Func<It.IsAnyType, Exception?, string>>((v, t) => true)), Times.Exactly(2));
    }

    [Fact]
    public async void Should_Call_Publish()
    {
        var smsReportDto = _dtoStub.Results.First();
        NotificationEvent notificationEvent = new(
            smsReportDto,
            _notificationId,
            smsReportDto.Status.GroupName.ToString(),
            NotificationType.Sms,
            ProvidersNotification.InfoBip);

        NotificationEvent paramReceivedMock = default!;
        _mediatorMock.Setup(x => x.Publish(It.IsAny<NotificationEvent>(), It.IsAny<CancellationToken>()))
            .Callback<NotificationEvent, CancellationToken>((@event, token) => paramReceivedMock = @event);

        await _infobipWebhookController.Receive(_dtoStub, _notificationId);

        _mediatorMock.Verify(x => x.Publish(It.IsAny<NotificationEvent>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
        notificationEvent.Should().BeEquivalentTo(paramReceivedMock);
    }

    [Fact]
    public async void Should_Return_Ok()
    {
        var response = await _infobipWebhookController.Receive(_dtoStub, _notificationId);
        response.Should().BeOfType<OkResult>();
    }
}
