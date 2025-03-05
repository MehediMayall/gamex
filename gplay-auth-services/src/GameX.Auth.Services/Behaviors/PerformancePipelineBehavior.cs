using System.Diagnostics;

namespace gamex.Auth.Services;


public sealed class PerformancePipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer;

    public PerformancePipelineBehavior()
    {
        _timer = new Stopwatch();
    }

     

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();
        var response = await next();
        _timer.Stop();
        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        // Log a warning if the request takes more than 5 seconds
        if (elapsedMilliseconds > 5000)
        {
            var requestName = typeof(TRequest).Name;
            Log.Warning("Long ET: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}", requestName, elapsedMilliseconds, request);
        }
        return response;
    }
}
