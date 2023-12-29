using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Api.External.Application.Events;
using Notification.Api.External.Core.Enums;
using Notification.Api.External.Webhook.Dtos;
using Notification.Api.External.Webhook.Filters;

namespace Notification.Api.External.Webhook.Controllers.v1;
public class WhatsAppWebhookController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<WhatsAppWebhookController> _logger;

    public WhatsAppWebhookController(IMediator mediator, ILogger<WhatsAppWebhookController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("{productId}")]
    [ServiceFilter(typeof(IpWhitelistFilter))]
    public async Task<IActionResult> Receive([FromBody] WhatsAppDeliveryDto dto, string productId)
    {
        _logger.LogInformation("Dto about WhatsApp Delivery Event received: {dto}", dto);

        await _mediator.Publish(new NotificationEvent(
            dto,
            productId,
            dto.Type.ToString(),
            NotificationType.WhatsApp,
            ProvidersNotification.WhatsApp
            ));

        _logger.LogInformation("Sms WhatsApp Event processed successfully: {dto}", dto);
        return Ok();
    }

    [HttpPost("{productId}")]
    [ServiceFilter(typeof(IpWhitelistFilter))]
    public async Task<IActionResult> Registering([FromBody] WhatsAppDeliveryDto dto, string productId)
    {
        _logger.LogInformation("Dto about WhatsApp Delivery Event received: {dto}", dto);

         

        _logger.LogInformation("Sms WhatsApp Event processed successfully: {dto}", dto);
        return Ok();
    }
}
