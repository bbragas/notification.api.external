using Notification.Api.External.Core.Enums;

namespace Notification.Api.External.Webhook.Dtos;

public class WhatsAppDeliveryDto
{
    public string Webhook { get; set; }
    public WhatsAppEventTypes Type { get; set; }
}
