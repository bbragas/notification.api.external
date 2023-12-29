using Notification.Api.External.Core.Contracts;

namespace Notification.Api.External.Eventbus.Envelopes;
public record Envelope(
    Guid Id,
    string SpecVersion,
    string Type,
    string Source,
    string Subject,
    string DataContentType,
    string Data,
    DateTime Time,
    string? DataSchema) : IEnvelope;