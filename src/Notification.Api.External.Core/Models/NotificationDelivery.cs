using Notification.Api.External.Core.Contracts;

namespace Notification.Api.External.Core.Models;
public class NotificationDelivery : IIdentifier
{
    public NotificationDelivery()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; }
    public string NotificationId { get; init; } = default!;
    public string Provider { get; init; } = default!;
    public string EventType { get; init; } = default!;
    public string NotificationType { get; init; } = default!;
    public object Content { get; init; } = default!;
    public DateTime CreatedAt { get; }
}