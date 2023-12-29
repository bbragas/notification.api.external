namespace Notification.Api.External.Application.Events.v1;
public record SmsExpiredEvent(NotificationEvent Content) : NotificationEventBase(Content)
{
    public override string Type => GetType().FullName!;

    public override string Description => "The message has been sent and has either expired due to pending past its validity period (our platform default is 48 hours), or the delivery report from the operator has reverted the expired as a final status";

    public override string? Schema
        => $"https://vegait.visualstudio.com/Notification/_git/notification.api.external?path=/schemas/{nameof(SmsExpiredEvent)}.json";
}
