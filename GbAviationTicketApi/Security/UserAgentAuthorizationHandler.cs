using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GbAviationTicketApi.Security
{
    public class UserAgentAuthorizationHandler : AuthorizationHandler<UserAgentRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAgentAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserAgentRequirement requirement)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var userRole = context.User.Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault()?.Value;

            var agent = httpContext?.Request.Headers["User-Agent"].ToString() ?? "";
            if (requirement.UserAgents.Contains(agent))
            {
                var isValidMobileUser = agent.Contains("android") && (userRole == "OPERATOR" || userRole == "ADMIN");
                var isValidDesktopUser = agent.Contains("win64") && (userRole == "ARP_AGENT" || userRole == "ADMIN");
                if (isValidDesktopUser || isValidMobileUser)
                    context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }

}
