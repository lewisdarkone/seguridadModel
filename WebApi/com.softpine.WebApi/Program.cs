using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using com.softpine.muvany.core;
using com.softpine.muvany.infrastructure;
using com.softpine.muvany.infrastructure.Filters;
using com.softpine.muvany.infrastructure.Identity.Services;
using com.softpine.muvany.infrastructure.SharedServices;
using com.softpine.muvany.WebApi.Host.Controllers;
using com.softpine.muvany.WebApi.Configurations;
using com.softpine.muvany.models.Interfaces;

[assembly: ApiConventionType(typeof(MuvanyApiConventions))]

StaticLogger.EnsureInitialized();
Log.Information("Server Booting Up...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    //Template Configurations
    builder.Host.AddConfigurations();
    builder.Host.UseSerilog((_, config) =>
    {
        config.WriteTo.Console()
        .ReadFrom.Configuration(builder.Configuration);
    });

    builder.Services.AddControllers(options =>
    {

    })

    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    });



    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddSingleton<IPasswordService, PasswordService>();

    builder.Services.AddSingleton<IUriService>(provider =>
    {
        var accesor = provider.GetRequiredService<IHttpContextAccessor>();
        if ( accesor.HttpContext != null )
        {
            var request = accesor.HttpContext.Request;
            var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
            return new UriService(absoluteUri);

        }
        return new UriService(null);
    });

    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });



    builder.Services.AddMvc(options =>
    {
        options.Filters.Add<ValidationFilter>();
    }).AddFluentValidation(options =>
    {
        var currentDomain = new List<Assembly>();
        foreach ( var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic) )
        {
            currentDomain.Add(assembly);
        }
        options.RegisterValidatorsFromAssemblies(currentDomain);
    });



    //Uso de AutoMapper
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddApplication();

    var app = builder.Build();

    //await app.Services.InitializeDatabasesAsync();

    app.UseInfrastructure(builder.Configuration);
    app.MapEndpoints();


    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



    app.UseHttpsRedirection();
    app.Run();

}
catch ( Exception ex ) when ( !ex.GetType().Name.Equals("StopTheHostException", StringComparison.Ordinal) )
{
    StaticLogger.EnsureInitialized();
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    StaticLogger.EnsureInitialized();
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}
