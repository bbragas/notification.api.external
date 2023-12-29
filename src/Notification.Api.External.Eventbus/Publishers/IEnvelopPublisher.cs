using Notification.Api.External.Core.Contracts;
using Notification.Api.External.Eventbus.Configurations;
using Refit;

namespace Notification.Api.External.Eventbus.Publishers;

[Headers(CustomHttpHeaders.UserAgent)]
public interface IEnvelopPublisher
{
    [Put("/api/v1/publish")]
    Task<HttpResponseMessage> PublishAsync(IEnvelope envelope, CancellationToken cancellationToken);
}
