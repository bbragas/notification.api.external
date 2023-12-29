using MongoDB.Driver;

namespace Notification.Api.External.Data.Context;
public interface IDbContext
{
    IMongoCollection<T> GetCollection<T>();
}
