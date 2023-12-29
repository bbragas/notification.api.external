using System.Net;

namespace Notification.Api.External.Webhook.Errors.ErrorDetailCreators;

public class DefaultExceptionDetailCreator : IExceptionDetailCreator
{
    private readonly ILogger<DefaultExceptionDetailCreator> _logger;
    public DefaultExceptionDetailCreator(ILogger<DefaultExceptionDetailCreator> logger)
    {
        _logger = logger;
    }

    public Type Type => typeof(Exception);

    // code smell detected is a sonarqube bug, see link bellow.
    // https://github.com/dotnet/roslyn-analyzers/issues/5626
    // the project was marked as <NoWarn>CA2254</NoWarn>
    // to avoid this code smell
    public ErrorDetail GetErrorDetail(Exception exception)
    {
        _logger.LogError("{@message}", exception.Message);

        return new ErrorDetail((int)HttpStatusCode.InternalServerError, "Internal server error");
    }
}