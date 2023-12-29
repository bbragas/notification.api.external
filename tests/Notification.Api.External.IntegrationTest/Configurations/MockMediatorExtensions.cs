using MediatR;
using Moq;

namespace Notification.Api.External.IntegrationTests.Configurations;

public static class MockMediatorExtensions
{
    public static void SetupMediatorPublishWithSuccess(this Mock<IMediator> mediator) 
        => mediator.Setup(x => x.Publish(It.IsAny<object>(), It.IsAny<CancellationToken>()));
}
