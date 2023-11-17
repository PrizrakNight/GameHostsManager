using FluentValidation;
using GameHostsManager.Application.Contracts.HostRooms;
using GameHostsManager.Application.Options;

namespace GameHostsManager.Infrastructure.Validation.Validators.HostRoomInfos
{
    public class SetCurrentPlayersContractsValidator : AbstractValidator<SetCurrentPlayersContract>
    {
        public SetCurrentPlayersContractsValidator(PlayerOptions playerOptions)
        {
            RuleFor(x => x.CurrentPlayers)
                .NotEmpty()
                .InclusiveBetween((ushort)0, playerOptions.MaxPlayers);
        }
    }
}
