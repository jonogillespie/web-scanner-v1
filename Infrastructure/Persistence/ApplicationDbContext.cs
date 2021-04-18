using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(
            DbContextOptions options) : base(options)
        {
        }

        public DbSet<Website> Websites { get; set; }
        public DbSet<ScanResult> ScanResults { get; set; }
        public DbSet<ScanCycle> ScanCycles { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<Entity>())
                // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.DateModified = DateTime.Now;
                        break;
                    case EntityState.Added:
                        entry.Entity.DateCreated = DateTime.Now;
                        entry.Entity.DateModified = DateTime.Now;
                        break;
                }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}