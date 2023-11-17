using GameHostsManager.Application.Contracts.HostRooms;
using GameHostsManager.Application.Exceptions;
using GameHostsManager.Application.Repositories;
using GameHostsManager.Domain;

namespace GameHostsManager.Application.Services.HostRooms
{
    public class HostRoomService : IHostRoomService
    {
        private readonly IHostRoomRepository _repository;
        private readonly IMappingService _mapping;
        private readonly ICurrentUserService _currentUser;
        private readonly ICancellationTokenProvider _tokenProvider;

        public HostRoomService(IHostRoomRepository repository,
            IMappingService mapping,
            ICurrentUserService currentUser,
            ICancellationTokenProvider tokenProvider)
        {
            _repository = repository;
            _mapping = mapping;
            _currentUser = currentUser;
            _tokenProvider = tokenProvider;
        }

        public async Task<HostRoomContract> CreateAsync(CreateHostRoomContract hostRoom)
        {
            var roomAlreadyExists = await _repository.ExistsByAddressOrNameAsync
            (
                ipAddress: hostRoom.IpAddress,
                port: hostRoom.Port,
                name: hostRoom.Name,
                cancellationToken: _tokenProvider.CancellationToken
            );

            if (roomAlreadyExists)
                throw BadOperationException.RoomAlreadyExists();

            var entity = _mapping.Map<HostRoom>(hostRoom);

            // TODO: Add password hasher
            entity.OwnerId = _currentUser.Id!.Value;

            var created = await _repository.AddAsync(entity, _tokenProvider.CancellationToken);

            if (created == null)
                throw ErrorOperationException.CreationRoomFailed();

            return _mapping.Map<HostRoomContract>(created);
        }

        public async Task DeleteAsync(Guid id)
        {
            var room = await _repository.GetAsync(id, _tokenProvider.CancellationToken);

            if (room == null || room.OwnerId != _currentUser.Id)
                throw BadOperationException.RoomNotFound();

            await _repository.DeleteAsync(id, _tokenProvider.CancellationToken);
        }

        public async Task<HostRoomConnectionInfoContract> GetConnectionInfoAsync(Guid id, string? password)
        {
            var foundRoom = await _repository.GetAsync(id, _tokenProvider.CancellationToken);

            if (foundRoom != null)
            {
                if (!string.IsNullOrWhiteSpace(foundRoom.Password))
                {
                    // TODO: Use password hasher
                    if (foundRoom.OwnerId == _currentUser.Id || foundRoom.Password == password)
                    {
                        return _mapping.Map<HostRoomConnectionInfoContract>(foundRoom);
                    }
                }
                else return _mapping.Map<HostRoomConnectionInfoContract>(foundRoom);
            }

            throw BadOperationException.RoomNotFound();
        }

        public async Task<List<HostRoomContract>> SearchAsync(HostRoomFilterContract filter)
        {
            var result = await _repository.GetByFilterAsync(new Dto.HostRoomFilterDto
            {
                OnlyPublic = filter.OnlyPublic,
                SearchString = filter.SearchString,
                Skip = filter.Pagination.Skip,
                Take = filter.Pagination.Take
            }, _tokenProvider.CancellationToken);

            return _mapping.Map<List<HostRoomContract>>(result);
        }

        public async Task SetCurrentPlayersAsync(Guid id, SetCurrentPlayersContract contract)
        {
            var foundRoom = await _repository.GetAsync(id, _tokenProvider.CancellationToken);

            if (foundRoom == null || foundRoom.OwnerId != _currentUser.Id)
                throw BadOperationException.RoomNotFound();

            foundRoom.CurrentPlayers = contract.CurrentPlayers;

            await _repository.UpdateAsync(foundRoom, _tokenProvider.CancellationToken);
        }
    }
}
