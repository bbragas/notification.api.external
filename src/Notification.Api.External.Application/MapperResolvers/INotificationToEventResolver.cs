using Notification.Api.External.Application.Events;

namespace Notification.Api.External.Application.MapperResolvers;
public interface INotificationToEventResolver : IMapperResolver<NotificationEvent, IEvent>
{
    bool ApplyTo(NotificationEvent reservation);
}