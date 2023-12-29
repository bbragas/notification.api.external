using Notification.Api.External.Application.Events;
using Notification.Api.External.Application.Events.v1;
using Notification.Api.External.Core.Enums;

namespace Notification.Api.External.Application.MapperResolvers.Resolvers;

public class NotificationSmsExpiredEventResolver : INotificationToEventResolver
{
    public bool ApplyTo(NotificationEvent reservation) =>
        reservation.EventType == SmsEventTypes.Expired.ToString();
    public IEvent Map(NotificationEvent source) => new SmsExpiredEvent(source);
}