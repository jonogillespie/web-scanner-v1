using System;

namespace Infrastructure.Services.Cron
{
    public class ScheduleConfig : IScheduleConfig
    {
        public string CronExpression { get; set; }
        public TimeZoneInfo TimeZoneInfo { get; set; }
    }
}