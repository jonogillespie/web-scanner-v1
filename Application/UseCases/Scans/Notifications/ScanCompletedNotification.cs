using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Scans.Notifications
{
    public class ScanCompletedNotification : INotification
    {
        public ScanCompletedNotification(IEnumerable<GoogleAnalyticsScanResponse> analyticsScanResponses,
            DateTime startDate,
            DateTime endDate)
        {
            AnalyticsScanResponses = analyticsScanResponses;
            StartDate = startDate;
            EndDate = endDate;
        }

        private IEnumerable<GoogleAnalyticsScanResponse> AnalyticsScanResponses { get; }
        private DateTime StartDate { get; }
        private DateTime EndDate { get; }

        // ReSharper disable once UnusedType.Global
        public class Handler : INotificationHandler<ScanCompletedNotification>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task Handle(ScanCompletedNotification notification,
                CancellationToken cancellationToken)
            {
                var entities = MapNotificationToEntities(notification);

                var scanCycle = new ScanCycle
                {
                    StartDate = notification.StartDate,
                    EndDate = notification.EndDate,
                    ScanResults = entities
                };

                await SaveScanCycleToDatabase(scanCycle, cancellationToken);
            }

            private List<ScanResult> MapNotificationToEntities(ScanCompletedNotification notification)
            {
                var successfulScans =
                    notification.AnalyticsScanResponses
                        .Where(x => x.HasSucceeded);

                var entities = _mapper
                    .Map<List<ScanResult>>(successfulScans);
                return entities;
            }

            private async Task SaveScanCycleToDatabase(ScanCycle scanCycle, CancellationToken cancellationToken)
            {
                await _context.ScanCycles.AddAsync(scanCycle,
                    cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}