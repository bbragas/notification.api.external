using System.Text;
using System.Text.Json;

namespace Notification.Api.External.Application.Events;
public abstract record NotificationEventBase(NotificationEvent Content) : IEvent
{
    public virtual Guid Id => Guid.NewGuid();

    public virtual string Version => "1.0";

    public virtual string Source => "Notification.Api.External";

    public virtual string ContentType => "application/json;base64";

    public abstract string Type { get; }

    public abstract string? Schema { get; }

    public abstract string Description { get; }

    public virtual string ToBase64Data(JsonSerializerOptions options)
        => Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(Content, options)));
}
