using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Notification.Api.External.Application.Events;
using Notification.Api.External.Application.MapperResolvers;
using Notification.Api.External.Application.MapperResolvers.Resolvers;
using System.Reflection;

namespace Notification.Api.External.Application.Configurations;
public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddSingleton<IMapperResolver<NotificationEvent, IEvent>, NotificationToEventMapper>()
            .AddSingleton<INotificationToEventResolver, NotificationSmsExpiredEventResolver>()
            .AddSingleton<INotificationToEventResolver, NotificationToEmailBounceEventResolver>()
            .AddSingleton<INotificationToEventResolver, NotificationToEmailClickEventResolver>()
            .AddSingleton<INotificationToEventResolver, NotificationToEmailReadEventResolver>()
            .AddSingleton<INotificationToEventResolver, NotificationToEmailUnsubscribeEventResolver>()
            .AddSingleton<INotificationToEventResolver, NotificationToSmsDeliveredEventResolver>()
            .AddSingleton<INotificationToEventResolver, NotificationToSmsPendingEventResolver>()
            .AddSingleton<INotificationToEventResolver, NotificationToSmsRejectedEventResolver>()
            .AddSingleton<INotificationToEventResolver, NotificationToSmsUndeliverableEventResolver>()
            .AddMediatR(typeof(DependencyInjectionConfiguration))
            .AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}
