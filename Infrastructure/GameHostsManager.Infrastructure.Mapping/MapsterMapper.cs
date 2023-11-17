using GameHostsManager.Application.Services;
using Mapster;

namespace GameHostsManager.Infrastructure.Mapping
{
    public class MapsterMapper : IMappingService
    {
        public TDestination Map<TDestination>(object source)
        {
            return source.Adapt<TDestination>();
        }

        public void Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            source.Adapt(destination);
        }
    }
}