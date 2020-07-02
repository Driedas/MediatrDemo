using Handlers.Behaviors;
using Messages.Security;
using System.Security.Claims;

namespace Handlers.Security
{
	public class AuthenticationService
		: IAuthenticationService
	{
		public bool HasPermission(ClaimsPrincipal principal, ApplicationPermission permission)
		{
			string userId = principal.UserId();
			// check permission store for userId and permission
			return true;
		}
	}
}
