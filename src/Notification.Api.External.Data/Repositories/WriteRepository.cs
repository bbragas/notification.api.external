using MongoDB.Driver;
using Notification.Api.External.Core.Contracts;
using Notification.Api.External.Data.Context;

namespace Notification.Api.External.Data.Repositories;
public class WriteRepository : IWriteRepository
{
    protected readonly IDbContext _mongoContext;
    private readonly Dictionary<Type, dynamic> repository = new();

    public WriteRepository(IDbContext context)
    {
        _mongoContext = context;
    }

    protected IMongoCollection<TEntity> GetCollection<TEntity>()
    {
        if (!repository.ContainsKey(typeof(TEntity)))
        {
            repository.Add(typeof(TEntity), _mongoContext.GetCollection<TEntity>());
        }

        return repository[typeof(TEntity)];
    }

    public Task CreateAsync<TEntity>(TEntity obj, CancellationToken cancellationToken) where TEntity : IIdentifier
    {
        if (obj is null)
            throw new ArgumentNullException($"{typeof(TEntity).Name} object is null");

        return CreateInternalAsync(obj, cancellationToken);
    }

    private async Task CreateInternalAsync<TEntity>(TEntity obj, CancellationToken cancellationToken) where TEntity : IIdentifier
        => await GetCollection<TEntity>().InsertOneAsync(obj, cancellationToken: cancellationToken);
}
