using Moq;
using Notification.Api.External.Core.Contracts;
using Notification.Api.External.Eventbus.Publishers;
using System.Net;

namespace Notification.Api.External.IntegrationTests.Configurations;

public static class MockApiEventBusRepositoryExtensions
{
    public static void SetupApiEventBusRepositoryPublishAsyncWithSuccess(this Mock<IEnvelopPublisher> apiEventBusRepositoryMock)
        => apiEventBusRepositoryMock
            .Setup(x => x.PublishAsync(It.IsAny<IEnvelope>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK
                });

}
