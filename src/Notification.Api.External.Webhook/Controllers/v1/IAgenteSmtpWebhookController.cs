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
public class IAgenteSmtpWebhookController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<IAgenteSmtpWebhookController> _logger;

    public IAgenteSmtpWebhookController(ILogger<IAgenteSmtpWebhookController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [ServiceFilter(typeof(IpWhitelistFilter))]
    public async Task<IActionResult> Receive([FromQuery] IAgenteSmtpDto dto)
    {
        _logger.LogInformation("Dto about Email Delivery Event received: {dto}", dto);

        await _mediator.Publish(new NotificationEvent(
            dto,
            dto.CampaignId,
            dto.Type.ToString(),
            NotificationType.Email,
            ProvidersNotification.IAgenteSmtp));

        _logger.LogInformation("Email Delivery Event processed successfully: {dto}", dto);

        return Ok();
    }
}
