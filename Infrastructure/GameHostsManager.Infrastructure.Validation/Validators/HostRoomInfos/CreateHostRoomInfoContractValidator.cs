using FluentValidation;
using GameHostsManager.Application.Contracts.HostRooms;
using GameHostsManager.Application.Options;
using GameHostsManager.Infrastructure.Validation.Extensions;

namespace GameHostsManager.Infrastructure.Validation.Validators.HostRoomInfos
{
    public class CreateHostRoomInfoContractValidator : AbstractValidator<CreateHostRoomContract>
    {
        public CreateHostRoomInfoContractValidator(PlayerOptions playerOptions)
        {
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
