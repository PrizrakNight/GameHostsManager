using FluentValidation;
using GameHostsManager.Application.Contracts;

namespace GameHostsManager.Infrastructure.Validation.Validators
{
    public class PaginationContractValidator : AbstractValidator<PaginationContract>
    {
        public PaginationContractValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 250);
        }
    }
}
