using System.Collections.Generic;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Application.UseCases.Scans.Queries.Dto
{
    public class ScanVm
    {
        public ScanCycleDto ScanCycle { get; init; }
        public List<ScanDto> Scans { get; init; }
    }
}