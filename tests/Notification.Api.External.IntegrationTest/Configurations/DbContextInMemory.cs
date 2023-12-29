using Mongo2Go;
using MongoDB.Driver;
using Notification.Api.External.Data.Context;

namespace Notification.Api.External.IntegrationTests.Configurations;

public class DbContextInMemory : IDbContext
{
    private readonly Lazy<IMongoDatabase> _db;
    internal static string _databaseName = "IntegrationTest";

    public DbContextInMemory()
    {
        var runner = MongoDbRunner.Start();

        _db = new Lazy<IMongoDatabase>(() =>
        {
            var mongoClient = new MongoClient(runner.ConnectionString);
            return mongoClient.GetDatabase(_databaseName);
        });
    }

    public IMongoCollection<T> GetCollection<T>()
    => _db.Value.GetCollection<T>(typeof(T).Name);
}
