using System;
using Application.Infrastructure.AutoMapper;
using AutoMapper;
using Domain.Entities;

namespace Application.UseCases.Scans.Queries.Dto
{
    public class ScanDto : IHaveCustomMapping
    {
        public string WebsiteUrl { get; set; }
        public string WebsiteName { get; set; }
        public bool HasGoogleAnalytics { get; set; }
        public TimeSpan ScanDuration { get; set; }
        public DateTime LastTimeScanned { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<ScanResult, ScanDto>()
                .ForMember(x => x.WebsiteUrl,
                    opt => opt.MapFrom(x => x.Website.Url))
                .ForMember(x => x.WebsiteName,
                    opt => opt.MapFrom(x => x.Website.Name))
                .ForMember(x => x.ScanDuration,
                    opt => opt.MapFrom(x => x.EndDate.Subtract(x.StartDate)))
                .ForMember(x => x.LastTimeScanned,
                    opt => opt.MapFrom(x => x.StartDate));
        }
    }
}