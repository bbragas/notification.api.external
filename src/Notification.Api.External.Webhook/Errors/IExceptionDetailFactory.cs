namespace Notification.Api.External.Webhook.Errors;

public interface IExceptionDetailFactory
{
    ErrorDetail GetErrorDetail(Exception exception);
}