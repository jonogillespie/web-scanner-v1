using System;
using System.Threading.Tasks;
using Application.Dto;

namespace Application.Interfaces
{
    public interface IGoogleAnalyticsScanService
    {
        Task<GoogleAnalyticsScanResponse> Scan(Uri uri, Guid websiteId);
    }
}