namespace Notification.Api.External.Application.Events.v1;
public record EmailReadEvent(NotificationEvent Content) : NotificationEventBase(Content)
{
    public override string Type => GetType().FullName!;

    public override string Description => "Whenever a recipient confirms the reading of a sent email (viewed images contained in the message)";

    public override string? Schema
        => $"https://vegait.visualstudio.com/Notification/_git/notification.api.external?path=/schemas/{nameof(EmailReadEvent)}.json";
}