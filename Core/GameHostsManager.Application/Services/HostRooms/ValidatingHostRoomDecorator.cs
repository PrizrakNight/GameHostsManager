using GameHostsManager.Application.Contracts.HostRooms;

namespace GameHostsManager.Application.Services.HostRooms
{
    public class ValidatingHostRoomDecorator : IHostRoomService
    {
        private readonly IHostRoomService _service;
        private readonly IValidationService _validationService;

        public ValidatingHostRoomDecorator(IHostRoomService service, IValidationService validationService)
        {
            _service = service;
            _validationService = validationService;
        }

        public async Task<HostRoomContract> CreateAsync(CreateHostRoomContract hostRoom)
        {
            await _validationService.ValidateAndThrowAsync(hostRoom);

            return await _service.CreateAsync(hostRoom);
        }

        public Task DeleteAsync(Guid id)
        {
            return _service.DeleteAsync(id);
        }

        public Task<HostRoomConnectionInfoContract> GetConnectionInfoAsync(Guid id, string? password)
        {
            return _service.GetConnectionInfoAsync(id, password);
        }

        public async Task<List<HostRoomContract>> SearchAsync(HostRoomFilterContract filter)
        {
            await _validationService.ValidateAndThrowAsync(filter);

            return await _service.SearchAsync(filter);
        }

        public async Task SetCurrentPlayersAsync(Guid id, SetCurrentPlayersContract contract)
        {
            await _validationService.ValidateAndThrowAsync(contract);

            await _service.SetCurrentPlayersAsync(id, contract);
        }
    }
}
