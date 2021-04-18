using System;
using System.Threading;
using System.Threading.Tasks;
using Cronos;
using Microsoft.Extensions.Hosting;
using Timer = System.Timers.Timer;

namespace Infrastructure.Services.Cron
{
    public abstract class CronJobService : IHostedService, IDisposable
    {
        private readonly CronExpression _expression;
        private readonly TimeZoneInfo _timeZoneInfo;
        private Timer _timer;

        protected CronJobService(string cronExpression, TimeZoneInfo timeZoneInfo)
        {
            _expression = CronExpression.Parse(cronExpression);
            _timeZoneInfo = timeZoneInfo;
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return ScheduleJob(cancellationToken);
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Stop();
            await Task.CompletedTask;
        }

        private async Task ScheduleJob(CancellationToken cancellationToken)
        {
            var next = _expression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);
            if (next.HasValue)
            {
                var delay = next.Value - DateTimeOffset.Now;

                if (delay.TotalMilliseconds <= 0) await ScheduleJob(cancellationToken);

                _timer = new Timer(delay.TotalMilliseconds);

                _timer.Elapsed += async (_, _) =>
                {
                    _timer.Dispose();
                    _timer = null;

                    if (!cancellationToken.IsCancellationRequested) await DoWork(cancellationToken);

                    if (!cancellationToken.IsCancellationRequested) await ScheduleJob(cancellationToken);
                };

                _timer.Start();
            }

            await Task.CompletedTask;
        }

        protected virtual async Task DoWork(CancellationToken cancellationToken)
        {
            await Task.Delay(5000, cancellationToken);
        }
    }
}