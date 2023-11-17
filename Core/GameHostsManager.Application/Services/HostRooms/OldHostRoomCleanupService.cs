using GameHostsManager.Application.Options;
using GameHostsManager.Application.Repositories;
using Microsoft.Extensions.Logging;

namespace GameHostsManager.Application.Services.HostRooms
{
    public class OldHostRoomCleanupService : IHostRoomCleanupService
    {
        private readonly IHostRoomRepository _repository;
        private readonly ICancellationTokenProvider _tokenProvider;

        private readonly ILogger<OldHostRoomCleanupService> _logger;

        private readonly HostRoomCleanupOptions _options;

        public OldHostRoomCleanupService(IHostRoomRepository repository,
            HostRoomCleanupOptions options,
            ICancellationTokenProvider tokenProvider,
            ILogger<OldHostRoomCleanupService> logger)
        {
            _repository = repository;
            _options = options;
            _tokenProvider = tokenProvider;
            _logger = logger;
        }

        public async Task CleanupAsync()
        {
            _logger.LogInformation("Cleaning of old rooms has been started.");

            var oldRooms = await _repository.GetByFilterAsync(new Dto.HostRoomFilterDto
            {
                OldFirsts = true,
                Take = _options.CleanupSize
            }, _tokenProvider.CancellationToken);

            if (oldRooms.Any() == false)
            {
                _logger.LogDebug("No cleaning rooms available, no cleaning required.");

                return;
            }

            _logger.LogDebug($"'{oldRooms.Count}' rooms for potential removing.");

            var utcNow = DateTime.UtcNow;
            var deletedRooms = 0;

            foreach (var oldRoom in oldRooms)
            {
                var needDelete = (utcNow - oldRoom.Updated).TotalSeconds >= _options.RoomLifetimeInSeconds;

                if (needDelete)
                {
                    // TODO: The best way to delete is bulk deletion
                    // But at the moment, this option is also suitable
                    await _repository.DeleteAsync(oldRoom.Id, _tokenProvider.CancellationToken);

                    deletedRooms++;
                }
            }

            _logger.LogDebug($"'{deletedRooms}' rooms have been removed.");

            _logger.LogInformation("Cleaning of the old rooms is completed.");
        }
    }
}
