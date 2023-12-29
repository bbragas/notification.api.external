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
public class IAgenteSmtpWebhookControllerTests
{
    private readonly IAgenteSmtpDto _dtoStub;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<ILogger<IAgenteSmtpWebhookController>> _loggerMock;
    private readonly IAgenteSmtpWebhookController _iAgenteSmtpWebhookController;

    public IAgenteSmtpWebhookControllerTests()
    {
        _loggerMock = new();
        _mediatorMock = new();
        _iAgenteSmtpWebhookController = new(_loggerMock.Object, _mediatorMock.Object);

        _dtoStub = new Mock<IAgenteSmtpDto>().Object;
    }

    [Fact]
    public async void Should_Call_LogInformation()
    {
        await _iAgenteSmtpWebhookController.Receive(_dtoStub);

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
        NotificationEvent notificationEvent = new(
            _dtoStub,
            _dtoStub.CampaignId,
            _dtoStub.Type.ToString(),
            NotificationType.Email,
            ProvidersNotification.IAgenteSmtp);

        NotificationEvent paramReceivedMock = default!;
        _mediatorMock.Setup(x => x.Publish(It.IsAny<NotificationEvent>(), It.IsAny<CancellationToken>()))
            .Callback<NotificationEvent, CancellationToken>((@event, token) => paramReceivedMock = @event);

        await _iAgenteSmtpWebhookController.Receive(_dtoStub);

        _mediatorMock.Verify(x => x.Publish(It.IsAny<NotificationEvent>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
        notificationEvent.Should().BeEquivalentTo(paramReceivedMock);
    }

    [Fact]
    public async void Should_Return_Ok()
    {
        var response = await _iAgenteSmtpWebhookController.Receive(_dtoStub);
        response.Should().BeOfType<OkResult>();
    }
}
