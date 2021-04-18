using System;
using Application.Infrastructure.AutoMapper;
using AutoMapper;
using Domain.Entities;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Application.Dto
{
    public class GoogleAnalyticsScanResponse : IHaveCustomMapping
    {
        public static GoogleAnalyticsScanResponse Unsuccessful => new() {HasSucceeded = false};
        public bool HasGoogleAnalytics { get; init; }
        public DateTime StartDate { get; init; }
        public bool HasSucceeded { get; init; } = true;
        public DateTime EndDate { get; init; }
        public Guid WebsiteId { get; init; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<GoogleAnalyticsScanResponse, ScanResult>();
        }
    }
}