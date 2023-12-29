using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Notification.Api.External.Data.CollectionMappers;
using Notification.Api.External.Data.Configurations;

namespace Notification.Api.External.Data.Context;
public class DbContext : IDbContext
{
    private readonly Lazy<IMongoDatabase> _db;

    public DbContext(IOptions<Mongo> configuration, IEnumerable<ICollectionMapper> collectionMappers)
    {
        _db = new Lazy<IMongoDatabase>(() =>
        {
            var mongoClient = new MongoClient(configuration.Value.ConnectionString);
            return mongoClient.GetDatabase(configuration.Value.DatabaseName);
        });

        CreateMapper(collectionMappers);
    }

    private static void CreateMapper(IEnumerable<ICollectionMapper> collectionMappers)
    {
        foreach (var collectionMapper in collectionMappers)
            collectionMapper.Map();
    }

    public IMongoCollection<T> GetCollection<T>()
        => _db.Value.GetCollection<T>(typeof(T).Name);
}
