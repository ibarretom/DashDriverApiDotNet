using Microsoft.EntityFrameworkCore;

namespace Infra.Database;

internal class DashDriverContext : DbContext
{
    public DashDriverContext(DbContextOptions<DashDriverContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DashDriverContext).Assembly);
    }
}
