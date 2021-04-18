using System.Reflection;
using Application.Infrastructure.AutoMapper;
using Application.Infrastructure.Mediator;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependencyInjection).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddAutoMapper(typeof(AutoMapperProfile).GetTypeInfo().Assembly);
        }
    }
}