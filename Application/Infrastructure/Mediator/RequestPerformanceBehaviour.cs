using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Infrastructure.Mediator
{
    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        #region Static Fields and Constants

        private const int PerformanceThreshold = 500;

        #endregion

        #region Constructors

        public RequestPerformanceBehaviour(ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();
            _logger = logger;
        }

        #endregion

        #region Interface Implementations

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            if (_timer.ElapsedMilliseconds <= PerformanceThreshold) return response;
            var name = typeof(TRequest).Name;

            _logger.LogWarning(
                "Long Running Request: {Date} {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
                DateTime.Now, name, _timer.ElapsedMilliseconds, request);

            return response;
        }

        #endregion

        #region Fields

        private readonly ILogger<TRequest> _logger;
        private readonly Stopwatch _timer;

        #endregion
    }
}