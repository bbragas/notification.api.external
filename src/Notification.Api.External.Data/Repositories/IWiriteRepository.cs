using Notification.Api.External.Core.Contracts;

namespace Notification.Api.External.Data.Repositories;
public interface IWriteRepository
{
    Task CreateAsync<TEntity>(TEntity obj, CancellationToken cancellationToken) where TEntity : IIdentifier;
}
