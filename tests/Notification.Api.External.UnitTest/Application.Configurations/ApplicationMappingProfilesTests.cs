using AutoMapper;
using Notification.Api.External.Application.Configurations;

namespace Notification.Api.External.UnitTests.Application.Configurations;
public class ApplicationMappingProfilesTests
{
    [Fact]
    public void Should_Not_Throw_Exception_If_MappingProfile_Is_Correct()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<ApplicationMappingProfiles>());
        config.AssertConfigurationIsValid();
    }
}
