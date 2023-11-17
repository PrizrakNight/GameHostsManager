using GameHostsManager.Application.Dto;
using GameHostsManager.Domain;

namespace GameHostsManager.Application.Repositories
{
    public interface IHostRoomRepository
    {
        ValueTask<bool> ExistsByAddressOrNameAsync(string ipAddress,
            ushort port,
            string? name,
            CancellationToken cancellationToken = default);

        ValueTask<List<HostRoom>> GetByFilterAsync(HostRoomFilterDto filter,
            CancellationToken cancellationToken = default);

        ValueTask<HostRoom?> GetAsync(Guid id,
            CancellationToken cancellationToken = default);

        ValueTask<HostRoom?> AddAsync(HostRoom hostInfo,
            CancellationToken cancellationToken = default);

        ValueTask<HostRoom?> UpdateAsync(HostRoom hostInfo,
            CancellationToken cancellationToken = default);

        ValueTask DeleteAsync(Guid id,
            CancellationToken cancellationToken = default);
    }
}
