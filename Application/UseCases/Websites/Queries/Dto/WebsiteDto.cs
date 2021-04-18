using System;
using Application.Infrastructure.AutoMapper;
using AutoMapper;
using Domain.Entities;

namespace Application.UseCases.Websites.Queries.Dto
{
    public class WebsiteDto : IHaveCustomMapping
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Website, WebsiteDto>();
        }
    }
}