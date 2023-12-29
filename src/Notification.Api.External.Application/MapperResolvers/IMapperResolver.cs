namespace Notification.Api.External.Application.MapperResolvers;
public interface IMapperResolver<in TSource, out TDestination>
{
    TDestination Map(TSource source);
}