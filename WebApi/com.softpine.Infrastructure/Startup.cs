using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using com.softpine.muvany.infrastructure.Identity;
using com.softpine.muvany.infrastructure.SecurityHeaders;
using com.softpine.muvany.infrastructure.Cors;
using com.softpine.muvany.infrastructure.OpenApi;
using com.softpine.muvany.infrastructure.Identity.Options;
using com.softpine.muvany.infrastructure.Caching;
using com.softpine.muvany.infrastructure.SharedServices.Mailing;
using com.softpine.muvany.infrastructure.Persistence;
using com.softpine.muvany.infrastructure.SharedServices;
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.Interfaces;

namespace com.softpine.muvany.infrastructure;

/// <summary>
/// 
/// </summary>
public static class Startup
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
            .AddApiVersioning()
            .AddAuth(config)
            .AddCaching(config)
            .AddCorsPolicy(config)
            .AddExceptionMiddleware()
            .AddMailing(config)
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()))
            //.AddMediatR(Assembly.GetExecutingAssembly())
            .AddOpenApiDocumentation(config)
            .AddIdentityPersistence(config)
            .AddPersistence(config)
            .AddSuscriptionMongo(config)
            .AddRouting(options => options.LowercaseUrls = true)
            .AddOptions(config)
            //.AddBackgroundJobs(config)
            .AddServices();
    }

    private static IServiceCollection AddApiVersioning(this IServiceCollection services) =>
        services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
        });

    private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PaginationOptions>(options => configuration.GetSection("Pagination").Bind(options));
        services.Configure<PasswordOptions>(options => configuration.GetSection("PasswordOptions").Bind(options));
        services.Configure<ConfigurationsConstants>(options => configuration.GetSection("ConfigurationsConstants").Bind(options));
        return services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration config) =>
        builder
            .UseRequestLocalization()
            .UseSecurityHeaders(config)
            .UseFileStorage()
            .UseExceptionMiddleware()
            .UseRouting()
            .UseCorsPolicy()
            .UseStaticFiles()
            .UseAuthentication()
            .UseCurrentUser()
            .UseAuthorization()
            .UseOpenApiDocumentation(config);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapControllers().RequireAuthorization();
        //builder.MapHealthCheck();
        return builder;
    }

    private static IEndpointConventionBuilder MapHealthCheck(this IEndpointRouteBuilder endpoints) =>
        endpoints.MapHealthChecks("/api/health").RequireAuthorization();

    internal static IServiceCollection AddServices(this IServiceCollection services)
    {

        //services.AddTransient<ISerializerService, NewtonSoftService>();
        services
            .AddServices(typeof(ITransientService), ServiceLifetime.Transient)
            .AddServices(typeof(IScopedService), ServiceLifetime.Scoped);

        return services;


    }

    internal static IServiceCollection AddServices(this IServiceCollection services, Type interfaceType, ServiceLifetime lifetime)
    {
        var interfaceTypes =
            AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(t => interfaceType.IsAssignableFrom(t)
                            && t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterfaces().FirstOrDefault(),
                    Implementation = t
                })
                .Where(t => t.Service is not null
                            && interfaceType.IsAssignableFrom(t.Service));

        foreach ( var type in interfaceTypes )
        {
            services.AddService(type.Service!, type.Implementation, lifetime);
        }

        return services;
    }

    internal static IServiceCollection AddService(this IServiceCollection services, Type serviceType, Type implementationType, ServiceLifetime lifetime) =>
        lifetime switch
        {
            ServiceLifetime.Transient => services.AddTransient(serviceType, implementationType),
            ServiceLifetime.Scoped => services.AddScoped(serviceType, implementationType),
            ServiceLifetime.Singleton => services.AddSingleton(serviceType, implementationType),
            _ => throw new ArgumentException("Invalid lifeTime", nameof(lifetime))
        };

    internal static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app, IConfiguration config)
    {
        var settings = config.GetSection(nameof(SecurityHeaderSettings)).Get<SecurityHeaderSettings>();

        if ( settings?.Enable is true )
        {
            app.Use(async (context, next) =>
            {
                if ( !context.Response.HasStarted )
                {
                    if ( !string.IsNullOrWhiteSpace(settings.XFrameOptions) )
                    {
                        context.Response.Headers.Add(HeaderNames.XFRAMEOPTIONS, settings.XFrameOptions);
                    }

                    if ( !string.IsNullOrWhiteSpace(settings.XContentTypeOptions) )
                    {
                        context.Response.Headers.Add(HeaderNames.XCONTENTTYPEOPTIONS, settings.XContentTypeOptions);
                    }

                    if ( !string.IsNullOrWhiteSpace(settings.ReferrerPolicy) )
                    {
                        context.Response.Headers.Add(HeaderNames.REFERRERPOLICY, settings.ReferrerPolicy);
                    }

                    if ( !string.IsNullOrWhiteSpace(settings.PermissionsPolicy) )
                    {
                        context.Response.Headers.Add(HeaderNames.PERMISSIONSPOLICY, settings.PermissionsPolicy);
                    }

                    if ( !string.IsNullOrWhiteSpace(settings.SameSite) )
                    {
                        context.Response.Headers.Add(HeaderNames.SAMESITE, settings.SameSite);
                    }

                    if ( !string.IsNullOrWhiteSpace(settings.XXSSProtection) )
                    {
                        context.Response.Headers.Add(HeaderNames.XXSSPROTECTION, settings.XXSSProtection);
                    }
                }

                await next();
            });
        }

        return app;
    }

    internal static IApplicationBuilder UseFileStorage(this IApplicationBuilder app) =>
        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Files")),
            RequestPath = new PathString("/Files")
        });




}
