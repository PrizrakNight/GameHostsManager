using GameHostsManager.Application.Services;
using GameHostsManager.WebApi.Constants;

namespace GameHostsManager.WebApi.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid? Id
        {
            get
            {
                if (_contextAccessor?.HttpContext == null)
                    throw new InvalidOperationException("HttpContext should not be Null");

                if (!_cachedUserId.HasValue)
                {
                    if (_contextAccessor.HttpContext.Request.Headers.TryGetValue(GameHostsHttpHeaders.UserIdentity, out var value))
                    {
                        _cachedUserId = Guid.Parse(value!);

                        return _cachedUserId;
                    }
                }

                return _cachedUserId;
            }
        }

        private Guid? _cachedUserId;
    }
}
