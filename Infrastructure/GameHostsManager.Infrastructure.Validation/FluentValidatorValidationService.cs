using FluentValidation;
using GameHostsManager.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using GameHostsManager.Application.Exceptions;

namespace GameHostsManager.Infrastructure.Validation
{
    public class FluentValidatorValidationService : IValidationService
    {
        private readonly IServiceProvider _serviceProvider;

        public FluentValidatorValidationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ValidateAndThrowAsync<T>(T @object,
            CancellationToken cancellationToken = default)
        {
            var validators = _serviceProvider.GetServices<IValidator<T>>();
            var context = new ValidationContext<T>(@object);
            var validationTasks = validators.Select(validator => validator.ValidateAsync(context));
            var validationFailures = await Task.WhenAll(validationTasks);

            var errors = validationFailures
                .Where(validationResult => !validationResult.IsValid)
                .SelectMany(validationResult => validationResult.Errors)
                .Select(validationFailure => new BadOperationException.ValidationError
                {
                    PropertyName = validationFailure.PropertyName,
                    ErrorMessage = validationFailure.ErrorMessage
                })
                .ToList();

            if (errors.Any())
            {
                throw BadOperationException.ValidationProblems(errors);
            }
        }
    }
}
