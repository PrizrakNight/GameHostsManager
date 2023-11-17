using FluentValidation;
using GameHostsManager.Application.Options;
using GameHostsManager.Application.Repositories;
using GameHostsManager.Application.Services;
using GameHostsManager.Application.Services.HostRooms;
using GameHostsManager.Infrastructure.Mapping.MapsterProfiles;
using GameHostsManager.Infrastructure.Validation;
using GameHostsManager.Infrastructure.Validation.Validators;
using GameHostsManager.Persistance.MemoryCache;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GameHostsManager.Infrastructure.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMappings(this IServiceCollection services,
            IConfiguration configuration)
        {
            var allRegisters = TypeAdapterConfig.GlobalSettings.Scan(typeof(HostRoomInfoRegister).Assembly);

            TypeAdapterConfig.GlobalSettings.Apply(allRegisters);

            services.AddScoped<IMappingService, Mapping.MapsterMapper>();

            return services;
        }

        public static IServiceCollection AddFluentValidation(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddValidatorsFromAssemblyContaining<PaginationContractValidator>();

            services.AddScoped<IValidationService, FluentValidatorValidationService>();

            return services;
        }

        public static IServiceCollection AddPersistanceMemoryCache(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IHostRoomRepository, HostRoomInfoMemoryCacheRepository>();

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<PlayerOptions>(configuration.GetSection(nameof(PlayerOptions)));
            services.Configure<HostRoomCleanupOptions>(configuration.GetSection(nameof(HostRoomCleanupOptions)));

            services.AddScoped(sp => sp.GetRequiredService<IOptionsSnapshot<PlayerOptions>>().Value);
            services.AddScoped(sp => sp.GetRequiredService<IOptionsSnapshot<HostRoomCleanupOptions>>().Value);

            services.AddScoped<IHostRoomCleanupService, OldHostRoomCleanupService>();

            services.AddScoped<IHostRoomService, HostRoomService>();
            services.Decorate<IHostRoomService, ValidatingHostRoomDecorator>();

            return services;
        }
    }
}