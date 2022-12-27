using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Beth.Catalog.Api.Infrastructure.ServiceConfiguration;

public static class CatalogDbContextServiceConfiguration
{
    public static async Task<IServiceCollection> AddCatalogDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("CatalogDb");
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        await using var dataSource = dataSourceBuilder.Build();
        
        return services.AddDbContext<CatalogContext>(options => options.UseNpgsql(dataSource));
    }
}