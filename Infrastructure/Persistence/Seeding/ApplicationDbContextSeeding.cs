using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Entities;

namespace Infrastructure.Persistence.Seeding
{
    public static class ApplicationDbContextSeeding
    {
        public static async Task SeedWebsiteDataFromCsv(this IApplicationDbContext context, string path)
        {
            var websites = GetWebsitesFromCsv(path);
            await context.Websites.AddRangeAsync(websites);
            await context.SaveChangesAsync(CancellationToken.None);
        }

        private static IEnumerable<Website> GetWebsitesFromCsv(string path)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null,
                MissingFieldFound = null
            };

            using var streamReader = new StreamReader(path);
            using var csvReader = new CsvReader(streamReader, config);
            csvReader.Context.RegisterClassMap<WebsiteMap>();
            return csvReader.GetRecords<Website>().ToList();
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        private sealed class WebsiteMap : ClassMap<Website>
        {
            public WebsiteMap()
            {
                Map(m => m.Name)
                    .Name("Name");
                Map(m => m.Url)
                    .Name("Website");
            }
        }
    }
}