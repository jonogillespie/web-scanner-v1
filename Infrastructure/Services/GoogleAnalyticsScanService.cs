using System;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Dto;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class GoogleAnalyticsScanService : IGoogleAnalyticsScanService
    {
        private readonly IConfiguration _configuration;

        public GoogleAnalyticsScanService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<GoogleAnalyticsScanResponse> Scan(Uri uri, Guid websiteId)
        {
            var dateStarted = DateTime.Now;

            using var http = new HttpClient();

            var res = await http.GetAsync(uri);
            if (!res.IsSuccessStatusCode)
                return GoogleAnalyticsScanResponse.Unsuccessful;

            var html = await res.Content.ReadAsStringAsync();

            var response = new GoogleAnalyticsScanResponse
            {
                StartDate = dateStarted,
                EndDate = DateTime.Now,
                HasGoogleAnalytics = html.Contains(_configuration["GoogleAnalytics:Url"]),
                HasSucceeded = true,
                WebsiteId = websiteId
            };

            return response;
        }
    }
}