using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.infrastructure.Identity.Models;
using com.softpine.muvany.infrastructure.Persistence.DataAccess;
using com.softpine.muvany.infrastructure.Persistence.EFContexts;
using com.softpine.muvany.infrastructure.Persistence.EFRepositories;
using com.softpine.muvany.infrastructure.Repositories;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using com.softpine.muvany.core.Services.Suscripciones;

namespace com.softpine.muvany.infrastructure.Persistence;

internal static class Startup
{

    private static readonly ILogger _logger = Log.ForContext(typeof(Startup));

    //Identity Configurations

    internal static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        var databaseSettings = config.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();
        string rootConnectionString = databaseSettings.ConnectionString;
        string dbProvider = databaseSettings.DBProvider;
        

        return services
            .Configure<DatabaseSettings>(config.GetSection(nameof(DatabaseSettings)))
            .AddDbContext<LenderesContext>(options => options.UseMySQL(rootConnectionString,
                    b => b.MigrationsAssembly(typeof(LenderesContext).Assembly.FullName)))
            .AddTransient<IDatabaseConnectionFactory>(e =>
            {
                return new SqlConnectionFactory(databaseSettings);
            })

            .AddRepositories();
    }

    internal static IServiceCollection AddSuscriptionMongo(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddScoped<ISuscripcionCatalogService, SuscripcionCatalogServices>();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IBaseRepositoryDapper, BaseRepositoryDapper>();
        return services;
    }
}
