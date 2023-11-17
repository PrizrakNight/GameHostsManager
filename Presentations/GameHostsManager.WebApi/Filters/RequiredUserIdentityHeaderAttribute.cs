using GameHostsManager.WebApi.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GameHostsManager.WebApi.Filters
{
    public class RequiredUserIdentityHeaderAttribute : ActionFilterAttribute
    {
        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey(GameHostsHttpHeaders.UserIdentity))
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

                var missingUserIdentityHeader = new ProblemDetails
                {
                    Title = $"Missing '{GameHostsHttpHeaders.UserIdentity}' header",
                    Type = "MissingUserIdentityHeader",
                    Status = StatusCodes.Status401Unauthorized
                };

                await context.HttpContext.Response.WriteAsJsonAsync(missingUserIdentityHeader);
            }
        }
    }
}
