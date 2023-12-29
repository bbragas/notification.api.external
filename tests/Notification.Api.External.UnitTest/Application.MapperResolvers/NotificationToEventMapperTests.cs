using Notification.Api.External.Application.MapperResolvers;
using Bogus;
using Notification.Api.External.Application.Events;
using Notification.Api.External.Core.Enums;
using Notification.Api.External.Core.Exceptions;

namespace Notification.Api.External.UnitTests.Application.MapperResolvers;
public class NotificationToEventMapperTests
{
    private readonly Faker _faker;
    private readonly NotificationEvent _commandStub;

    public NotificationToEventMapperTests()
    {
        _faker = new();
        _commandStub = new NotificationEvent(
            _faker.Person,
            _faker.Random.Guid().ToString(),
            _faker.Random.Enum<EmailEventTypes>().ToString(),
            NotificationType.Email,
            ProvidersNotification.IAgenteSmtp);
    }

    [Fact]
    public void Should_Throw_Exception_If_No_Mapper_Defined_To_Resolve()
    {
        var resolvers = Enumerable.Empty<INotificationToEventResolver>();

        Assert.Throws<MapperUndefinedException>(() =>
            new NotificationToEventMapper(resolvers).Map(_commandStub));
    }
}
