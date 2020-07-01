using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Handlers.Behaviors
{
	public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
			{
				var result = await next().ConfigureAwait(false);

				scope.Complete();

				return result;
			}
		}
	}
}