using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Notification.Api.External.Webhook.Infrastructure.Configurations;
using Notification.Api.External.Webhook.Infrastructure.HostBuilders;
using Notification.Api.External.Webhook.Infrastructure.Settings;
using Notification.Api.External.Application.Configurations;
using Notification.Api.External.Data.Configurations;
using Notification.Api.External.Eventbus.Configurations;
using Notification.Api.External.Webhook.Configurations;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);



        builder.Configuration.AddJsonFile("appsettings.json", true).AddEnvironmentVariables();

        builder.Host.ConfigureLog();

        builder.Services.AddVersioning()
            .AddSettings(builder.Configuration)
            .AddHealthChecks(builder.Configuration)
            .AddEndpointsApiExplorer()
            .AddSwagger()
            .AddApplication()
            .AddExceptionServices()
            .AddEventBusDependencies(builder.Configuration)
            .AddFiltersConfiguration(builder.Configuration)
            .AddRepositoryDependencies(builder.Configuration)
            .AddControllers()
            .AddNewtonsoftJson();

        var app = builder.Build();

        app.UseSwagger(app.Services.GetService<IOptions<EnvironmentSettings>>()!.Value, app.Services.GetService<IApiVersionDescriptionProvider>()!);

        app.UseAuthorization();

        app.UseHealthCheck();

        app.MapControllers();

        app.UseExceptionConfigure(app.Services);

        app.Run();
    }
}