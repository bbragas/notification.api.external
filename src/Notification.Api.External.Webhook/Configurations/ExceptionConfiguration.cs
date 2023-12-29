using Notification.Api.External.Webhook.Errors;
using Notification.Api.External.Webhook.Errors.ErrorDetailCreators;

namespace Notification.Api.External.Webhook.Configurations;

public static class ExceptionConfiguration
{
    public static IServiceCollection AddExceptionServices(this IServiceCollection services)
    {
        services
            .AddSingleton<IExceptionDetailFactory, ExceptionDetailFactory>()
            .AddSingleton<IExceptionDetailCreator, DefaultExceptionDetailCreator>()
            .AddSingleton<IExceptionDetailCreator, EnvelopPublishingExceptionDetailCreator>();

        return services;
    }

    public static void UseExceptionConfigure(this IApplicationBuilder app, IServiceProvider services)
    {
        app.UseExceptionHandler(appError =>
        {
            var exceptionDetailFactory = services.GetService<IExceptionDetailFactory>();
            if (exceptionDetailFactory is not null)
                appError.Use(ExceptionMiddleware(exceptionDetailFactory));
        });
    }

    private static Func<HttpContext, RequestDelegate, Task> ExceptionMiddleware(IExceptionDetailFactory exceptionDetailFactory) =>
        (httpContext, next) => ExceptionMiddlewareWriteResponse.WriteResponse(httpContext, exceptionDetailFactory);
}
