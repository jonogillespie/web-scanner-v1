using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Dto;
using Application.Interfaces;
using Application.UseCases.Scans.Notifications;
using Application.UseCases.Websites.Queries;
using Application.UseCases.Websites.Queries.Dto;
using Infrastructure.Services.Cron;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Tasks
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ScanTask : CronJobService
    {
        private readonly IServiceProvider _serviceProvider;

        public ScanTask(IScheduleConfig scheduleConfig,
            IServiceProvider serviceProvider)
            : base(scheduleConfig.CronExpression,
                scheduleConfig.TimeZoneInfo)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task DoWork(CancellationToken cancellationToken)
        {
            var startDate = DateTime.Now;

            var websites = await GetWebsites(cancellationToken);

            var websiteList = CreateWebsiteScanTasks(websites);

            var results =
                (await Task.WhenAll(websiteList)).AsEnumerable();

            await NotifyTaskCompleted(results, startDate, cancellationToken);
        }

        private async Task NotifyTaskCompleted(IEnumerable<GoogleAnalyticsScanResponse> results,
            DateTime startDate,
            CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator =
                scope.ServiceProvider
                    .GetRequiredService<IMediator>();

            await mediator.Publish(new ScanCompletedNotification(results,
                    startDate,
                    DateTime.Now),
                cancellationToken);
        }

        private IEnumerable<Task<GoogleAnalyticsScanResponse>> CreateWebsiteScanTasks(IEnumerable<WebsiteDto> websites)
        {
            using var scope = _serviceProvider.CreateScope();
            var googleAnalyticsScanService =
                scope.ServiceProvider
                    .GetRequiredService<IGoogleAnalyticsScanService>();

            var websiteList =
                websites
                    .Select(x =>
                        googleAnalyticsScanService.Scan(new Uri($"http://{x.Url}"), x.Id));
            return websiteList;
        }

        private async Task<List<WebsiteDto>> GetWebsites(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider
                .GetRequiredService<IMediator>();

            return await mediator
                .Send(new GetWebsitesQuery(), cancellationToken);
        }
    }
}