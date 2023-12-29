using Notification.Api.External.Webhook.Filters;

namespace Notification.Api.External.Webhook.Configurations;

public static class FiltersConfiguration
{
    public static IServiceCollection AddFiltersConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IpWhitelistFilter>();
        services.Configure<IpWhitelistSettings>(configuration.GetRequiredSection(nameof(IpWhitelistSettings)));

        return services;
    }
}
