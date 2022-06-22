using Microsoft.AspNetCore.Authorization;

namespace Carmarket.Infrastructure.Security.Policy.Requirement
{
    public class PolicyRequirementAdminOnly : AuthorizationHandler<PolicyRequirementAdminOnly>, IAuthorizationRequirement
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context, PolicyRequirementAdminOnly requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" && c.Value == "admin"))
            {
                context.Fail();
            }
            else
            {
                context.Succeed(requirement);
            }
            return System.Threading.Tasks.Task.FromResult(0);
        }
    }

}
