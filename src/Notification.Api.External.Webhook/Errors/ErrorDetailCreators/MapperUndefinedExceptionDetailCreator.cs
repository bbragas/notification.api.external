using Notification.Api.External.Core.Exceptions;
using System.Net;

namespace Notification.Api.External.Webhook.Errors.ErrorDetailCreators;

public class MapperUndefinedExceptionDetailCreator : IExceptionDetailCreator
{
    public Type Type => typeof(MapperUndefinedException);

    public ErrorDetail GetErrorDetail(Exception exception) => new((int)HttpStatusCode.InternalServerError, exception.Message);
}