using Coravel;
using GameHostsManager.Application.Services;
using GameHostsManager.WebApi.CoravelInvocables;
using GameHostsManager.WebApi.Services;

namespace GameHostsManager.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScheduler();
            services.AddHttpContextAccessor();

            services.AddTransient<HostRoomsCleanupInvocable>();

            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ICancellationTokenProvider, CancellationTokenProvider>();

            return services;
        }
    }
}
