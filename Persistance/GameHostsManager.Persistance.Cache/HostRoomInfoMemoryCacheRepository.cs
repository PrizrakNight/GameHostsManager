using GameHostsManager.Application.Dto;
using GameHostsManager.Application.Repositories;
using GameHostsManager.Application.Services;
using GameHostsManager.Domain;
using System.Collections.Concurrent;

namespace GameHostsManager.Persistance.MemoryCache
{
    public class HostRoomInfoMemoryCacheRepository : IHostRoomRepository
    {
        private static readonly ConcurrentDictionary<Guid, HostRoom> _cache;

        private readonly IMappingService _mappingService;

        static HostRoomInfoMemoryCacheRepository()
        {
            _cache = new ConcurrentDictionary<Guid, HostRoom>();
        }

        public HostRoomInfoMemoryCacheRepository(IMappingService mappingService)
        {
            _mappingService = mappingService;
        }

        public ValueTask<HostRoom?> AddAsync(HostRoom hostInfo,
            CancellationToken cancellationToken = default)
        {
            if (_cache.TryAdd(hostInfo.Id, hostInfo))
            {
                return ValueTask.FromResult<HostRoom?>(hostInfo);
            }

            return ValueTask.FromResult<HostRoom?>(null);
        }

        public ValueTask DeleteAsync(Guid id,
            CancellationToken cancellationToken = default)
        {
            _cache.TryRemove(id, out _);

            return ValueTask.CompletedTask;
        }

        public ValueTask<List<HostRoom>> GetByFilterAsync(HostRoomFilterDto filter,
            CancellationToken cancellationToken = default)
        {
            var query = _cache
                .Select(x => x.Value)
                .AsEnumerable();

            if (filter.OldFirsts.HasValue)
            {
                if (filter.OldFirsts.Value)
                {
                    query = query
                        .OrderByDescending(x => x.Updated);
                }
                else
                {
                    query = query
                        .OrderBy(x => x.Updated);
                }
            }

            if (!string.IsNullOrWhiteSpace(filter.IpAddress))
            {
                query = query
                    .Where(x => x.IpAddress == filter.IpAddress);
            }

            if (filter.Port.HasValue)
            {
                query = query
                    .Where(x => x.Port == filter.Port.Value);
            }

            if (filter.HostInfoIds?.Any() == true)
            {
                query = query
                    .Where(x => filter.HostInfoIds.Contains(x.Id));
            }

            if (filter.OwnerIds?.Any() == true)
            {
                query = query
                    .Where(x => filter.OwnerIds.Contains(x.Id));
            }

            if (!string.IsNullOrWhiteSpace(filter.Password))
            {
                query = query
                    .Where(x => x.Password == filter.Password);
            }

            if (filter.OnlyPublic.HasValue)
            {
                if (filter.OnlyPublic.Value)
                {
                    query = query
                        .Where(x => string.IsNullOrWhiteSpace(x.Password));
                }
                else
                {
                    query = query
                        .Where(x => !string.IsNullOrWhiteSpace(x.Password));
                }
            }

            if (!string.IsNullOrWhiteSpace(filter.SearchString))
            {
                var comparison = StringComparison.OrdinalIgnoreCase;

                query = query
                    .Where(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.Contains(filter.SearchString, comparison)
                        || !string.IsNullOrWhiteSpace(x.Description) && x.Description.Contains(filter.SearchString, comparison));
            }

            if (filter.Skip.HasValue)
            {
                query = query
                    .Skip(filter.Skip.Value);
            }

            if (filter.Take.HasValue)
            {
                query = query
                    .Take(filter.Take.Value);
            }

            return ValueTask.FromResult(query.ToList());
        }

        public ValueTask<HostRoom?> UpdateAsync(HostRoom hostInfo,
            CancellationToken cancellationToken = default)
        {
            if (_cache.TryGetValue(hostInfo.Id, out var forUpdate))
            {
                _mappingService.Map(hostInfo, forUpdate);

                forUpdate.Updated = DateTime.UtcNow;

                return ValueTask.FromResult<HostRoom?>(forUpdate);
            }

            return ValueTask.FromResult<HostRoom?>(null);
        }

        public ValueTask<HostRoom?> GetAsync(Guid id,
            CancellationToken cancellationToken = default)
        {
            if (_cache.TryGetValue(id, out var hostInfo))
            {
                return ValueTask.FromResult<HostRoom?>(hostInfo);
            }

            return ValueTask.FromResult<HostRoom?>(null);
        }

        public ValueTask<bool> ExistsByAddressOrNameAsync(string ipAddress,
            ushort port,
            string? name,
            CancellationToken cancellationToken = default)
        {
            var exists = _cache
                .Select(x => x.Value)
                .Any(x => x.IpAddress == ipAddress && x.Port == port || x.Name == name);

            return ValueTask.FromResult(exists);
        }
    }
}