using Notification.Api.External.Core.Exceptions;
using System.Net;

namespace Notification.Api.External.Webhook.Errors.ErrorDetailCreators;

public class EnvelopPublishingExceptionDetailCreator : IExceptionDetailCreator
{
    public Type Type => typeof(EnvelopPublishingException);

    public ErrorDetail GetErrorDetail(Exception exception) => new((int)HttpStatusCode.InternalServerError, exception.Message);
}