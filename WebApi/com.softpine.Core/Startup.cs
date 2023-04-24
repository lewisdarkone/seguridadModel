using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace com.softpine.muvany.core;

/// <summary>
/// 
/// </summary>
public static class Startup
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        return services
            .AddValidatorsFromAssembly(assembly)
            .AddMediatR( cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            //.AddMediatR(assembly);
    }
}
