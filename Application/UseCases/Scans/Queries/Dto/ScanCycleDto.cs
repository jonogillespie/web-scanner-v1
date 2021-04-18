using System;
using Application.Infrastructure.AutoMapper;
using AutoMapper;
using Domain.Entities;

namespace Application.UseCases.Scans.Queries.Dto
{
    public class ScanCycleDto : IHaveCustomMapping
    {
        public DateTime DateStarted { get; set; }
        public DateTime DateCompleted { get; set; }
        public TimeSpan ScanTime { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<ScanCycle, ScanCycleDto>()
                .ForMember(x => x.DateCompleted,
                    opt => opt.MapFrom(x => x.EndDate))
                .ForMember(x => x.DateStarted,
                    opt => opt.MapFrom(x => x.StartDate))
                .ForMember(x => x.ScanTime,
                    opt => opt.MapFrom(x => x.EndDate.Subtract(x.StartDate)));
        }
    }
}