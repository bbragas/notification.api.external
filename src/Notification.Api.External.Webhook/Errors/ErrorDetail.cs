using System.Text.Json;

namespace Notification.Api.External.Webhook.Errors;

public class ErrorDetail
{
    public ErrorDetail(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }

    public int StatusCode { get; set; }
    public string Message { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this);
}
