using Microsoft.AspNetCore.Authorization;

namespace RowdyTroll.Api.Security
{
    public class HasScopeRequirement : IAuthorizationRequirement
    {
        public string Scope { get; }

        public string Issuer { get; }

        public HasScopeRequirement(string scope, string issuer)
        {
            Scope = scope;
            Issuer = issuer;
        }
    }
}
