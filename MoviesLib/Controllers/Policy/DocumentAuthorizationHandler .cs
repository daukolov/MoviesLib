using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SportLeagueASPNetCoreTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace SportLeagueASPNetCoreTest.Controllers.Authotization
{
    public class DocumentAuthorizationHandler :
        AuthorizationHandler<SameAuthorRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       SameAuthorRequirement requirement)
        {

            var Name = httpContextAccessor.HttpContext.Request.Query["username"].ToString();

            if (context.User.Identity.IsAuthenticated && context.User.Identity?.Name.ToLower() == Name.ToLower())
            {
                context.Succeed(requirement);
            }
            else context.Fail();

            return Task.CompletedTask;
        }

        private readonly IHttpContextAccessor httpContextAccessor;

        public DocumentAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
    }

    public class SameAuthorRequirement : IAuthorizationRequirement 
    {

    }
}
