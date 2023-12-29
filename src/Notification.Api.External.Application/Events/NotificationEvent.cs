using MediatR;
using Notification.Api.External.Core.Enums;

namespace Notification.Api.External.Application.Events;
public class NotificationEvent : INotification
{
    public NotificationEvent(object content, string notificationId, string eventType, NotificationType notificationType, ProvidersNotification providersNotification)
    {
        Content = content;
        NotificationId = notificationId;
        EventType = eventType;
        NotificationType = notificationType.ToString();
        Provider = providersNotification.ToString();
    }
    public object Content { get; init; }
    public string NotificationId { get; init; }
    public string EventType { get; init; }
    public string NotificationType { get; init; }
    public string Provider { get; init; }
}