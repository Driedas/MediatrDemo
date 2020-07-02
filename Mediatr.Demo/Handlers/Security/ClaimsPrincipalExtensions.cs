using System.Linq;
using System.Security.Claims;

namespace Handlers.Security
{
	public static class ClaimsPrincipalExtensions
	{
		public static string UserId(this ClaimsPrincipal principal)
		{
			return principal.Claims
				.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
				?.Value;
		}
	}
}
