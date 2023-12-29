using Notification.Api.External.Webhook.Errors.ErrorDetailCreators;

namespace Notification.Api.External.Webhook.Errors;

public class ExceptionDetailFactory : IExceptionDetailFactory
{
    private readonly IReadOnlyDictionary<Type, IExceptionDetailCreator> _errorMap;

    public ExceptionDetailFactory(IEnumerable<IExceptionDetailCreator> exceptionDetailCreators)
        => _errorMap = exceptionDetailCreators.ToDictionary(p => p.Type);

    public ErrorDetail GetErrorDetail(Exception exception)
    {
        Type type = typeof(Exception);
        if (_errorMap.ContainsKey(exception.GetType()))
            type = exception.GetType();

        return _errorMap[type].GetErrorDetail(exception);
    }
}