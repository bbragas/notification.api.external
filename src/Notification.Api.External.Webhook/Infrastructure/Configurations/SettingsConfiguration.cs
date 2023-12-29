using Notification.Api.External.Webhook.Infrastructure.Settings;

namespace Notification.Api.External.Webhook.Infrastructure.Configurations;
public static class SettingsConfiguration
{
    public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EnvironmentSettings>(configuration.GetSection("Environment"));

        return services;
    }
}
