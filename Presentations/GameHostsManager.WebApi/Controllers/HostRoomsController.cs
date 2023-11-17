using GameHostsManager.Application.Contracts.HostRooms;
using GameHostsManager.Application.Services.HostRooms;
using GameHostsManager.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GameHostsManager.WebApi.Controllers
{
    [Route("api/host-rooms")]
    [ApiController]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public class HostRoomsController : ControllerBase
    {
        private readonly IHostRoomService _hostRoomService;

        public HostRoomsController(IHostRoomService hostRoomService)
        {
            _hostRoomService = hostRoomService;
        }

        /// <summary>
        /// Returns connection information
        /// </summary>
        [HttpPost("{roomId}/connection-info")]
        [ProducesResponseType(typeof(List<HostRoomContract>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetConnectionInfo(Guid roomId,
            [FromBody] GetConnectionInfoContract contract)
        {
            var result = await _hostRoomService.GetConnectionInfoAsync(roomId, contract.Password);

            return Ok(result);
        }

        /// <summary>
        /// Searches for rooms according to the specified filters
        /// </summary>
        [HttpPost("search")]
        [ProducesResponseType(typeof(List<HostRoomContract>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Search([FromBody] HostRoomFilterContract filter)
        {
            var result = await _hostRoomService.SearchAsync(filter);

            return Ok(result);
        }

        /// <summary>
        /// Creates a new room
        /// </summary>
        [HttpPost]
        [RequiredUserIdentityHeader]
        [ProducesResponseType(typeof(HostRoomContract), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add([FromBody] CreateHostRoomContract filter)
        {
            var result = await _hostRoomService.CreateAsync(filter);

            return Ok(result);
        }

        /// <summary>
        /// Sets the current number of players in the room
        /// </summary>
        [HttpPost("{roomId}/current-players")]
        [RequiredUserIdentityHeader]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SetCurrentPlayers(Guid roomId,
            [FromBody] SetCurrentPlayersContract contract)
        {
            await _hostRoomService.SetCurrentPlayersAsync(roomId, contract);

            return Ok();
        }

        /// <summary>
        /// Deletes a room
        /// </summary>
        [HttpDelete("{roomId}")]
        [RequiredUserIdentityHeader]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid roomId)
        {
            await _hostRoomService.DeleteAsync(roomId);

            return Ok();
        }
    }
}
