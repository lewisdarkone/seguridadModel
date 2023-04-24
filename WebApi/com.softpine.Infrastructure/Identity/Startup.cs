using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.infrastructure.Identity.Auth;
using com.softpine.muvany.infrastructure.Identity.Auth.Jwt;
using com.softpine.muvany.infrastructure.Identity.Auth.Permissions;
using com.softpine.muvany.infrastructure.Identity.Context;
using com.softpine.muvany.infrastructure.Identity.EFIdentityRepositories;
using com.softpine.muvany.infrastructure.Identity.Entities;
using com.softpine.muvany.infrastructure.Identity.Models;
using com.softpine.muvany.infrastructure.Repositories;
using com.softpine.muvany.models.Interfaces;

namespace com.softpine.muvany.infrastructure.Identity;

internal static class Startup
{

    private static readonly ILogger _logger = Log.ForContext(typeof(Startup));


    //Identity Configurations

    internal static IServiceCollection AddIdentityPersistence(this IServiceCollection services, IConfiguration config)
    {
        try
        {
        var databaseSettings = config.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();
        string rootConnectionString = databaseSettings.ConnectionString;
        string dbProvider = databaseSettings.DBProvider;

        

        return services
            .Configure<DatabaseSettings>(config.GetSection(nameof(DatabaseSettings)))
            .AddDbContext<IdentityContext>(options =>
            options.UseMySQL(rootConnectionString))
            .AddIdentityRepositories();
        }catch(Exception ex)
        {
            
              Console.WriteLine(ex.ToString());
            return null;
        }
    }

    //Identity Configurations
    internal static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddCurrentUser()
            .AddPermissions()
            // Must add identity before adding auth!
            .AddIdentity();
        services.Configure<SecuritySettings>(config.GetSection(nameof(SecuritySettings)));
        return config["SecuritySettings:Provider"].Equals("AzureAd", StringComparison.OrdinalIgnoreCase)
            ? services.AddJwtAuth(config)
            : services.AddJwtAuth(config);
    }

    internal static IApplicationBuilder UseCurrentUser(this IApplicationBuilder app) =>
        app.UseMiddleware<CurrentUserMiddleware>();

    private static IServiceCollection AddCurrentUser(this IServiceCollection services) =>
        services
            .AddScoped<CurrentUserMiddleware>()
            .AddScoped<ICurrentUser, CurrentUser>()
            .AddScoped(sp => (ICurrentUserInitializer)sp.GetRequiredService<ICurrentUser>());

    private static IServiceCollection AddPermissions(this IServiceCollection services) =>
        services
            .AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
            .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

    private static IServiceCollection AddIdentity(this IServiceCollection services) =>
        services
            .AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders()
            .Services;

    private static IServiceCollection AddIdentityRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepositoryIdentity<>), typeof(BaseRepositoryIdentity<>));
        services.AddScoped(typeof(IEndpointsRepository), typeof(EndpointsRepository));
        services.AddScoped(typeof(IPermisosRepository), typeof(PermisosRepository));
        services.AddScoped(typeof(IRecursosRepository), typeof(RecursosRepository));
        return services;
    }
}
