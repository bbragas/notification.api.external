using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.Api.External.Data.Configurations;

namespace Notification.Api.External.UnitTests.Data.Configurations;
public class DependencyInjectionConfigurationTests
{
    [Fact]
    public void AddRepositoryDependencies_Should_Throw_Exception_If_RequiredSection_Not_Found()
        => Assert.Throws<InvalidOperationException>(() => 
            new ServiceCollection().AddRepositoryDependencies(new ConfigurationBuilder().Build()));
}
