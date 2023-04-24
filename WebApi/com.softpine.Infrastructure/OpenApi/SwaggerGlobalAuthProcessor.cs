using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using NSwag;
using NSwag.Generation.AspNetCore;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace com.softpine.muvany.infrastructure.OpenApi;

internal static class ObjectExtensions
{
    public static T? TryGetPropertyValue<T>(this object? obj, string propertyName, T? defaultValue = default) =>
        obj?.GetType().GetRuntimeProperty(propertyName) is PropertyInfo propertyInfo
            ? (T?)propertyInfo.GetValue(obj)
            : defaultValue;
}

/// <summary>
/// The default NSwag AspNetCoreOperationProcessor doesn't take .RequireAuthorization() calls into account
/// Unless the AllowAnonymous attribute is defined, this processor will always add the security scheme
/// when it's not already there, so effectively adding "Global Auth".
/// </summary>
public class SwaggerGlobalAuthProcessor : IOperationProcessor
{
    private readonly string _name;

    /// <summary>
    /// 
    /// </summary>
    public SwaggerGlobalAuthProcessor()
        : this(JwtBearerDefaults.AuthenticationScheme)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    public SwaggerGlobalAuthProcessor(string name)
    {
        _name = name;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public bool Process(OperationProcessorContext context)
    {
        IList<object>? list = ((AspNetCoreOperationProcessorContext)context).ApiDescription?.ActionDescriptor?.TryGetPropertyValue<IList<object>>("EndpointMetadata");
        if (list is not null)
        {
            if (list.OfType<AllowAnonymousAttribute>().Any())
            {
                return true;
            }

            if (context.OperationDescription.Operation.Security?.Any() != true)
            {
                (context.OperationDescription.Operation.Security ??= new List<OpenApiSecurityRequirement>()).Add(new OpenApiSecurityRequirement
                {
                    {
                        _name,
                        Array.Empty<string>()
                    }
                });
            }
        }

        return true;
    }
}
