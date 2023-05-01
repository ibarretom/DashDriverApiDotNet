using Infra.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra;

public static class Bootstraper
{
    public static void AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DashDriverContext>(options =>
        {
            options.UseMySql(configuration.GetConnectionString("DefaultConnection"), 
                new MySqlServerVersion(new Version(8, 0, 26)), 
                b => b.MigrationsAssembly("Api"));
        });
    }
}
