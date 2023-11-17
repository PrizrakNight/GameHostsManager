namespace GameHostsManager.Application.Services
{
    public interface IMappingService
    {
        TDestination Map<TDestination>(object source);

        void Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}
