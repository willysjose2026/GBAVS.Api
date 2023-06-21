using Microsoft.AspNetCore.Authorization;

namespace GbAviationTicketApi.Security
{
    public class UserAgentRequirement : IAuthorizationRequirement
    {
        public List<string> UserAgents { get; }
        public UserAgentRequirement(List<string> userAgents) 
        {
            UserAgents = userAgents;
        }
    }
}
