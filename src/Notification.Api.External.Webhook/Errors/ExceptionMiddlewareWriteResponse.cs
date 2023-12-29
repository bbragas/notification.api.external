using Microsoft.AspNetCore.Diagnostics;

namespace Notification.Api.External.Webhook.Errors;

public static class ExceptionMiddlewareWriteResponse
{
    public static Task WriteResponse(HttpContext httpContext, IExceptionDetailFactory exceptionDetailFactory)
    {
        var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
        Exception? ex = exceptionDetails?.Error;

        if (ex is null)
            return Task.CompletedTask;

        ErrorDetail errorDetail = exceptionDetailFactory.GetErrorDetail(ex);

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = errorDetail.StatusCode;

        return httpContext.Response.WriteAsync(errorDetail.ToString());
    }
}
