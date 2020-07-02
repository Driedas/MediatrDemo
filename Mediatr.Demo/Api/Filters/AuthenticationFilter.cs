using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Security.Claims;

namespace Api.Filters
{
	public class AuthenticationFilter
		: IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			ClaimsIdentity identity = new ClaimsIdentity();
			identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()));
			ClaimsPrincipal principal = new ClaimsPrincipal(identity);

			context.HttpContext.User = principal;
		}
	}
}
