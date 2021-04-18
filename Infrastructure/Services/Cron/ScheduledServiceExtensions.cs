using System;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Cron
{
    public static class ScheduledServiceExtensions
    {
        public static void AddCronJob<T>(this IServiceCollection services,
            Action<IScheduleConfig> options) where T : CronJobService
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options), @"Please provide Schedule Configurations.");

            var config = new ScheduleConfig();

            options.Invoke(config);

            if (string.IsNullOrWhiteSpace(config.CronExpression))
                throw new ArgumentNullException(nameof(ScheduleConfig.CronExpression),
                    @"Empty Cron Expression is not allowed.");

            services.AddSingleton<IScheduleConfig>(config);
            services.AddHostedService<T>();
        }
    }
}