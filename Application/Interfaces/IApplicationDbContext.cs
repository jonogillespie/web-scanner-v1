using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Website> Websites { get; }

        // ReSharper disable once UnusedMember.Global
        DbSet<ScanResult> ScanResults { get; }
        DbSet<ScanCycle> ScanCycles { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}