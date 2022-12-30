using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace Beth.Catalog.Api.Infrastructure.ServiceConfiguration;

public static class CatalogDbContextServiceConfiguration
{
    public static async Task<IServiceCollection> AddCatalogDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CatalogContext>(options => options
            .UseNpgsql(connectionString)
        );
        services.AddScoped<DbContext, CatalogContext>();

        return services;
    }
}