using Bogus;
using Microsoft.Extensions.Options;
using Notification.Api.External.Data.Context;
using System.Data;

namespace Notification.Api.External.IntegrationTests.Configurations;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection Remove<T>(this IServiceCollection services)
    {
        if (services.IsReadOnly)
        {
            throw new ReadOnlyException($"{nameof(services)} is read only");
        }

        var serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(T));
        if (serviceDescriptor != null) services.Remove(serviceDescriptor);

        return services;
    }

    public static IServiceCollection ConfigureStartupFilter(this IServiceCollection services, IPAddressWrap ipWrap)
    {
        var ipWrapOptions = Options.Create(ipWrap);

        return services.AddSingleton<IStartupFilter, ChangeIpStartupFilter>(x => new(ipWrapOptions));
    }

    public static IServiceCollection AddDbContextInMemory(this IServiceCollection services)
        => services.AddSingleton<IDbContext, DbContextInMemory>();

}