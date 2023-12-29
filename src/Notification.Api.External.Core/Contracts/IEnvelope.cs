namespace Notification.Api.External.Core.Contracts;
public interface IEnvelope
{
    Guid Id { get; }
    string SpecVersion { get; }
    DateTime Time { get; }
    string Data { get; }
    string Source { get; }
    string DataContentType { get; }
    string? DataSchema { get; }
    string Type { get; }
    string Subject { get; }
}