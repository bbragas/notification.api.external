using MongoDB.Bson.Serialization;
using Notification.Api.External.Core.Models;

namespace Notification.Api.External.Data.CollectionMappers;
public class NotificationDeliveryCollectionMapper : ICollectionMapper
{
    public void Map()
    {
        BsonClassMap.RegisterClassMap<NotificationDelivery>(cm =>
        {
            cm.AutoMap();
        });
    }
}