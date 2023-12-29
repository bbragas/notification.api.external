using Microsoft.AspNetCore.Mvc;
using Notification.Api.External.Core.Enums;

namespace Notification.Api.External.Webhook.Dtos;

public class IAgenteSmtpDto
{
    [FromQuery(Name = "Data")]
    public string Date { get; set; } = default!;

    [FromQuery(Name = "CampanhaID")]
    public string CampaignId { get; set; } = default!;

    public string Email { get; set; } = default!;

    [FromQuery(Name = "Tipo")]
    public EmailEventTypes Type { get; set; }

    [FromQuery(Name = "Descricao")]
    public string Description { get; set; } = default!;

    [FromQuery(Name = "Assunto")]
    public string Subject { get; set; } = default!;
}