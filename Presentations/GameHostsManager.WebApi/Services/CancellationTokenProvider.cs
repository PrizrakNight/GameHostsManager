using GameHostsManager.Application.Services;

namespace GameHostsManager.WebApi.Services
{
    public class CancellationTokenProvider : ICancellationTokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CancellationTokenProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public CancellationToken CancellationToken
        {
            get
            {
                if (_contextAccessor.HttpContext == null)
                    return CancellationToken.None;

                return _contextAccessor.HttpContext.RequestAborted;
            }
        }
    }
}
