namespace Notification.Api.External.Application.Events.v1;
public record SmsUndeliverableEvent(NotificationEvent Content) : NotificationEventBase(Content)
{
    public override string Type => GetType().FullName!;
    public override string Description => "The message has not been delivered";
    public override string? Schema
        => $"https://vegait.visualstudio.com/Notification/_git/notification.api.external?path=/schemas/{nameof(SmsUndeliverableEvent)}.json";
}
