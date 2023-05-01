using Microsoft.EntityFrameworkCore;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Infra.Database;

internal class DashDriverContext : DbContext
{
    public DashDriverContext(DbContextOptions<DashDriverContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DashDriverContext).Assembly);

        modelBuilder.Entity<User>()
            .Property(x => x.CreatedAt);
    }
}
