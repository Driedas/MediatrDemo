using MediatR;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Handlers.Behaviors
{
	public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		public LoggingBehavior(/*some sort of logger*/)
		{

		}

		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			
			Trace.WriteLine($"handling request at {DateTime.UtcNow}");
			// serialize request data, minus sensitive fields
			
			var result = await next().ConfigureAwait(false);
			
			Trace.WriteLine($"request handled at {DateTime.UtcNow}, duration {stopwatch.Elapsed}");
			
			return result;
		}
	}
}
