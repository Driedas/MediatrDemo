using Autofac;
using Handlers.Behaviors;
using Handlers.Security;
using System.Security.Claims;

namespace Handlers.Configuration
{
	public class SecurityModule
		: Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<AuthenticationService>()
				.As<IAuthenticationService>();

			builder.Register<ClaimsPrincipal>(c => ClaimsPrincipal.Current);
		}
	}
}
