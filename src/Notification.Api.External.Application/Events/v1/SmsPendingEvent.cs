namespace Notification.Api.External.Application.Events.v1;
public record SmsPendingEvent(NotificationEvent Content) : NotificationEventBase(Content)
{
    public override string Type => GetType().FullName!;

    public override string Description => "The message has been processed and sent to the next instance, i.e., a mobile operator";

    public override string? Schema
        => $"https://vegait.visualstudio.com/Notification/_git/notification.api.external?path=/schemas/{nameof(SmsPendingEvent)}.json";
}
