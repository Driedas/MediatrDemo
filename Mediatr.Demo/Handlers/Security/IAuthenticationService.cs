using Messages.Security;
using System.Security.Claims;

namespace Handlers.Behaviors
{
	public interface IAuthenticationService
	{
		bool HasPermission(ClaimsPrincipal principal, ApplicationPermission permission);
	}
}
