
using Moq;
using Notification.Api.External.Core.Models;
using Notification.Api.External.Data.Context;
using Notification.Api.External.Data.Repositories;

namespace Notification.Api.External.UnitTests.Data.Repositories;
public class WriteRepositoryTests
{
    private readonly Mock<IDbContext> _dbContext;
    private readonly IWriteRepository _writeRepository;

    public WriteRepositoryTests()
    {
        _dbContext = new Mock<IDbContext>();
        _writeRepository = new WriteRepository(_dbContext.Object);
    }

    [Fact]
    public async Task Should_Throw_Exception_If_Param_Obj_Is_Null()
        => await Assert.ThrowsAsync<ArgumentNullException>(() 
            => _writeRepository.CreateAsync<NotificationDelivery>(null!, default));
}
