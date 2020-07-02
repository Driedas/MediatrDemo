using Handlers.Security;
using MediatR;
using Messages.Security;
using System;
using System.Reflection;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Handlers.Behaviors
{
	public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		private readonly ClaimsPrincipal principal;
		private readonly IAuthenticationService authService;

		public AuthorizationBehavior(ClaimsPrincipal principal, IAuthenticationService authService)
		{
			this.principal = principal;
			this.authService = authService;
		}

		public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			var permissionAttribute = request.GetType()
				.GetCustomAttribute<RequirePermissionAttribute>();

			if (permissionAttribute != null)
			{
				ApplicationPermission permission = permissionAttribute.Permission;
				if (!authService.HasPermission(this.principal, permission))
				{
					throw new UnauthorizedAccessException($"Principal {this.principal.UserId()} does not have permission {permission}.");
				}
			}

			return next();
		}
	}
}
