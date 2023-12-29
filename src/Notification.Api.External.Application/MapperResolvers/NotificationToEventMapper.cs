using Notification.Api.External.Application.Events;
using Notification.Api.External.Core.Exceptions;

namespace Notification.Api.External.Application.MapperResolvers;
public class NotificationToEventMapper : IMapperResolver<NotificationEvent, IEvent>
{
    private readonly IEnumerable<INotificationToEventResolver> _mapperResolvers;

    public NotificationToEventMapper(IEnumerable<INotificationToEventResolver> mapperResolvers)
    {
        _mapperResolvers = mapperResolvers;
    }

    public IEvent Map(NotificationEvent source)
    {
        var mapper = _mapperResolvers.FirstOrDefault(resolver => resolver.ApplyTo(source));

        if (mapper is null)
            throw new MapperUndefinedException($"No mapper defined to resolve {nameof(NotificationEvent)} with {source.EventType} status.");

        return mapper.Map(source);
    }
}
