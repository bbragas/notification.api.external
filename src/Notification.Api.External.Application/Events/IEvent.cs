using System.Text.Json;

namespace Notification.Api.External.Application.Events;
public interface IEvent
{
    Guid Id { get; }
    string Version { get; }
    string Source { get; }
    string ContentType { get; }
    string Type { get; }
    string Description { get; }
    string? Schema { get; }
    string ToBase64Data(JsonSerializerOptions options);
}