namespace GameHostsManager.Application.Services
{
    public interface IValidationService
    {
        Task ValidateAndThrowAsync<T>(T @object,
            CancellationToken cancellationToken = default);
    }
}
