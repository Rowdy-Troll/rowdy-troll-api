using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace RowdyTroll.Api.Security
{
    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "scope" || c.Type == ClaimTypes.Role || c.Type == "permissions"))
            {
                return Task.CompletedTask;
            }

            var scopeClaim = context.User.FindFirst(c => c.Type == "scope" || c.Type == "permissions");
            if (scopeClaim == null)
            {
                return Task.CompletedTask;
            }

            var scopes = scopeClaim.Value.Split(' ');
            if (scopes.Any(s => s == requirement.Scope))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
