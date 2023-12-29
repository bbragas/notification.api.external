using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.Api.External.Eventbus.Publishers;
using Refit;

namespace Notification.Api.External.Eventbus.Configurations;
public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddEventBusDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddRefitClient<IEnvelopPublisher>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(configuration.GetValue<string>("EventBusApiUrl"));
            });

        return services;
    }
}
