using FluentValidation;
using GameHostsManager.Application.Contracts.HostRooms;

namespace GameHostsManager.Infrastructure.Validation.Validators.HostRoomInfos
{
    public class HostRoomInfoFilterContractValidator : AbstractValidator<HostRoomFilterContract>
    {
        public HostRoomInfoFilterContractValidator()
        {
            RuleFor(x => x.Pagination)
                .SetValidator(new PaginationContractValidator());
        }
    }
}
