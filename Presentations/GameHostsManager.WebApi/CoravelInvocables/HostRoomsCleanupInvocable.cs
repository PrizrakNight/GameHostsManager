using Coravel.Invocable;
using GameHostsManager.Application.Services.HostRooms;

namespace GameHostsManager.WebApi.CoravelInvocables
{
    public class HostRoomsCleanupInvocable : IInvocable
    {
        private readonly IHostRoomCleanupService _cleanupService;

        public HostRoomsCleanupInvocable(IHostRoomCleanupService cleanupService)
        {
            _cleanupService = cleanupService;
        }

        public async Task Invoke()
        {
            await _cleanupService.CleanupAsync();
        }
    }
}
