namespace Notification.Api.External.Application.Events.v1;
public record EmailBounceEvent(NotificationEvent Content) : NotificationEventBase(Content)
{
    public override string Type => GetType().FullName!;

    public override string Description => "Whenever an email is rejected at the destination provider, reporting full mailbox, non-existent email etc";

    public override string? Schema 
        => $"https://vegait.visualstudio.com/Notification/_git/notification.api.external?path=/schemas/{nameof(EmailBounceEvent)}.json";
}