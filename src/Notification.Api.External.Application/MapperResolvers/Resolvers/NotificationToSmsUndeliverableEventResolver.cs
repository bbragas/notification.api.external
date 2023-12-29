using Notification.Api.External.Application.Events;
using Notification.Api.External.Application.Events.v1;
using Notification.Api.External.Core.Enums;

namespace Notification.Api.External.Application.MapperResolvers.Resolvers;

public class NotificationToSmsUndeliverableEventResolver : INotificationToEventResolver
{
    public bool ApplyTo(NotificationEvent reservation) =>
        reservation.EventType == SmsEventTypes.Undeliverable.ToString();
    public IEvent Map(NotificationEvent source) => new SmsUndeliverableEvent(source);
}