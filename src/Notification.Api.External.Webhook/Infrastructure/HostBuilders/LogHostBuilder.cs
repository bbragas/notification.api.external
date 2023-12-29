using Serilog;
using Serilog.Filters;
using Serilog.Formatting.Json;

namespace Notification.Api.External.Webhook.Infrastructure.HostBuilders;

public static class LogHostBuilder
{
    internal static IHostBuilder ConfigureLog(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((context, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                .Enrich.WithProperty("Application", "api")
                .Enrich.FromLogContext();

            configuration.Filter.ByExcluding(exclusion =>
            {
                var isHealthCheckEndpoint = Matching.WithProperty<string>("RequestPath", path => path.Contains("/health", StringComparison.InvariantCultureIgnoreCase));
                var isOkStatusCode = Matching.WithProperty<int>("StatusCode", code => code == 200);
                return isHealthCheckEndpoint(exclusion) && (exclusion.Properties.ContainsKey("StatusCode") ? isOkStatusCode(exclusion) : true);
            });


            configuration.WriteTo.Async(c => c.Console(new JsonFormatter(renderMessage: true)));
        });

        return hostBuilder;
    }
}