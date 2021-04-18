using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces;
using Application.UseCases.Scans.Queries.Dto;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Scans.Queries
{
    public class GetRecentScansQuery : IRequest<ScanVm>
    {
        // ReSharper disable once UnusedType.Global
        public class Handler : IRequestHandler<GetRecentScansQuery, ScanVm>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ScanVm> Handle(GetRecentScansQuery request,
                CancellationToken cancellationToken)
            {
                var latestScanCycle = await GetLatestScanCycle(cancellationToken);

                if (latestScanCycle == null) throw new NotFoundException("Latest scan could not be found.");

                return new ScanVm
                {
                    ScanCycle = _mapper.Map<ScanCycleDto>(latestScanCycle),
                    Scans = _mapper.Map<List<ScanDto>>(latestScanCycle.ScanResults)
                };
            }

            private async Task<ScanCycle> GetLatestScanCycle(CancellationToken cancellationToken)
            {
                var latestScanCycle =
                    await _context.ScanCycles
                        .Include(x => x.ScanResults)
                        .ThenInclude(x => x.Website)
                        .OrderByDescending(x => x.EndDate)
                        .FirstOrDefaultAsync(cancellationToken);
                return latestScanCycle;
            }
        }
    }
}