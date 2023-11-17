using FluentValidation;

namespace GameHostsManager.Infrastructure.Validation.Extensions
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, string> ShouldBeIPv4<T>(this IRuleBuilder<T, string> builder)
        {
            return builder
                .Matches(@"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}$")
                .WithMessage("The IP address must be IPv4");
        }
    }
}
