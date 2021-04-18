using System;
using Application.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Infrastructure.Services.Cron;
using Infrastructure.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("web-scanner-db"));

            services
                .AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services
                .AddSingleton<IGoogleAnalyticsScanService, GoogleAnalyticsScanService>();

            services.AddCronJob<ScanTask>(c =>
            {
                c.TimeZoneInfo = TimeZoneInfo.Local;
                c.CronExpression = "* * * * *";
            });
        }
    }
}