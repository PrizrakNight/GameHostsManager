using GameHostsManager.Application.Contracts.HostRooms;

namespace GameHostsManager.Application.Services.HostRooms
{
    public interface IHostRoomService
    {
        Task<List<HostRoomContract>> SearchAsync(HostRoomFilterContract filter);

        Task<HostRoomConnectionInfoContract> GetConnectionInfoAsync(Guid id,
            string? password);

        Task<HostRoomContract> CreateAsync(CreateHostRoomContract hostRoom);

        Task SetCurrentPlayersAsync(Guid id, SetCurrentPlayersContract contract);
        Task DeleteAsync(Guid id);
    }
}
