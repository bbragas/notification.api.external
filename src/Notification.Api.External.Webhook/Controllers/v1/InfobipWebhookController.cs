using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Api.External.Application.Events;
using Notification.Api.External.Core.Enums;
using Notification.Api.External.Webhook.Dtos;
using Notification.Api.External.Webhook.Filters;

namespace Notification.Api.External.Webhook.Controllers.v1;

[ApiVersion("1.0")]
[ApiController]
[Route("v{version:apiVersion}/[controller]")]
public class InfobipWebhookController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<InfobipWebhookController> _logger;

    public InfobipWebhookController(ILogger<InfobipWebhookController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost("{notificationId}")]
    [ServiceFilter(typeof(IpWhitelistFilter))]
    public async Task<IActionResult> Receive([FromBody] SmsDeliveryDto dto, string notificationId)
    {
        _logger.LogInformation("Dto about Sms Delivery Event received: {dto}", dto);

        var smsDeliveryResult = dto.Results.First();
        await _mediator.Publish(new NotificationEvent(
            smsDeliveryResult,
            notificationId,
            smsDeliveryResult.Status.GroupName.ToString(),
            NotificationType.Sms,
            ProvidersNotification.InfoBip));

        _logger.LogInformation("Sms Delivery Event processed successfully: {dto}", dto);

        return Ok();
    }
}
