using System;

namespace Infrastructure.Services.Cron
{
    public interface IScheduleConfig
    {
        string CronExpression { get; set; }
        TimeZoneInfo TimeZoneInfo { get; set; }
    }
}