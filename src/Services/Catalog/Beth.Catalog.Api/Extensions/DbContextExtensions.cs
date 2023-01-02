using System;
using System.Threading.Tasks;
using Beth.Catalog.Api.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Beth.Catalog.Api.Extensions;

public static class CatalogDbContextServiceConfiguration
{
    public static IServiceCollection AddCatalogDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CatalogContext>(options => options
            .UseNpgsql(connectionString)
        );
        services.AddScoped<DbContext, CatalogContext>();

        return services;
    }

    public static async Task<WebApplication> MigrateDbContext(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetService<CatalogContext>();
        var logger = services.GetRequiredService<ILogger<CatalogContext>>();

        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception e)
        {
            logger.LogError("Ошибка миграции БД: {e}", e);
            throw;
        }

        return app;
    }

    public static async Task<WebApplication> SeedCatalogDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetService<CatalogContext>();

        await CatalogContextSeed.SeedAsync(context);
        
        return app;
    }
}