using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
        (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse> // check GET if need IBaseRequest 
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handle request={Request} - response{Response} - requestData={RequestData}",
                typeof(TRequest).Name, typeof(TResponse).Name, request);

            // Performance Logging
            var timer = new Stopwatch();
            timer.Start();

            var response = await next();

            timer.Stop();
            var timeTaken = timer.Elapsed;
            if (timeTaken.Seconds > 3)
                logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken} seconds",
                    typeof(TRequest).Name, timeTaken.Seconds);

            logger.LogInformation("[END] Handled {Request} with {Response}",
                typeof(TRequest).Name, typeof(TResponse).Name);
            return response;
        }
    }
}
