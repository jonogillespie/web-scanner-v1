using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.UseCases.Websites.Queries.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Websites.Queries
{
    public class GetWebsitesQuery : IRequest<List<WebsiteDto>>
    {
        // ReSharper disable once UnusedType.Global
        public class Handler : IRequestHandler<GetWebsitesQuery, List<WebsiteDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<WebsiteDto>> Handle(GetWebsitesQuery request,
                CancellationToken cancellationToken)
            {
                return await _context.Websites
                    .ProjectTo<WebsiteDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}