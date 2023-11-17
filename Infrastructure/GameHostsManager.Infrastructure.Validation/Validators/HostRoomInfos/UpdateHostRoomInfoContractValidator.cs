using FluentValidation;
using GameHostsManager.Application.Contracts.HostRooms;
using GameHostsManager.Application.Options;
using GameHostsManager.Infrastructure.Validation.Extensions;

namespace GameHostsManager.Infrastructure.Validation.Validators.HostRoomInfos
{
    public class UpdateHostRoomInfoContractValidator : AbstractValidator<UpdateHostRoomContract>
    {
        public UpdateHostRoomInfoContractValidator(PlayerOptions playerOptions)
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.IpAddress)
                .NotEmpty()
                .ShouldBeIPv4();

            RuleFor(x => x.Port)
                .NotEmpty();

            RuleFor(x => x.MaxPlayers)
                .NotEmpty()
                .InclusiveBetween(playerOptions.MinPlayer, playerOptions.MaxPlayers);
        }
    }
}
