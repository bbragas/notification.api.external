namespace Notification.Api.External.Webhook.Configurations;

public class IpWhitelistSettings
{
    public List<string> EmailWhitelist { get; set; } = default!;
    public List<string> SmsWhitelist { get; set; } = default!;
}
