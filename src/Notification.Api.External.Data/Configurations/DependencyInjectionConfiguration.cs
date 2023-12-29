using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.Api.External.Data.CollectionMappers;
using Notification.Api.External.Data.Context;
using Notification.Api.External.Data.Repositories;

namespace Notification.Api.External.Data.Configurations;
public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddRepositoryDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCollectionMapper()
            .AddSingleton<IDbContext, DbContext>()
            .AddSingleton<IWriteRepository, WriteRepository>()
            .Configure<Mongo>(configuration.GetRequiredSection(nameof(Mongo)));

        return services;
    }

    private static IServiceCollection AddCollectionMapper(this IServiceCollection services)
    {
        Type type = typeof(ICollectionMapper);
        Type[] types = type.Assembly.GetTypes();

        foreach (Type item in types)
        {
            if (!item.IsInterface && typeof(ICollectionMapper).IsAssignableFrom(item))
                services.AddSingleton(type, item);
        }

        return services;
    }
}
