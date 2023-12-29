namespace Notification.Api.External.Webhook.Errors.ErrorDetailCreators;

public interface IExceptionDetailCreator
{
    public Type Type { get; }
    ErrorDetail GetErrorDetail(Exception exception);
}