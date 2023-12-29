namespace Notification.Api.External.Application.Events.v1;

public record EmailClickEvent(NotificationEvent Content) : NotificationEventBase(Content)
{
    public override string Type => GetType().FullName!;

    public override string Description => "Whenever a url is clicked in an email";

    public override string? Schema
        => $"https://vegait.visualstudio.com/Notification/_git/notification.api.external?path=/schemas/{nameof(EmailClickEvent)}.json";
}