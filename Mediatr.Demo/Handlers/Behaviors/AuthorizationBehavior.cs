using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Handlers.Behaviors
{
	public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			// perform authorization and throw or continue
			return next();
		}
	}
}
