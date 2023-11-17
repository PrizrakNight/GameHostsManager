namespace GameHostsManager.Application.Services
{
    public interface ICancellationTokenProvider
    {
        CancellationToken CancellationToken { get; }
    }
}
