using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Notification.Api.External.Webhook.Infrastructure.Settings;
using Notification.Api.External.Webhook.Infrastructure.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Notification.Api.External.Webhook.Infrastructure.Configurations;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.OperationFilter<DefaultOperationFilter>();
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();

        return services;
    }

    public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, EnvironmentSettings securityOptions, IApiVersionDescriptionProvider apiVersionProvider)
    {
        if (!securityOptions.HasSwagger) return app;

        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            foreach (var description in apiVersionProvider.ApiVersionDescriptions)
            {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/{description.GroupName}/swagger.json", $"Notification API External - {description.GroupName.ToUpper()}");
            }

        });

        foreach (var description in apiVersionProvider.ApiVersionDescriptions)
        {
            app.UseReDoc(c =>
            {
                c.DocumentTitle = "Notification API External - Docs";
                c.RoutePrefix = $"docs/{description.GroupName}";
                c.SpecUrl($"/swagger/{description.GroupName}/swagger.json");
            });
        }

        return app;
    }
}