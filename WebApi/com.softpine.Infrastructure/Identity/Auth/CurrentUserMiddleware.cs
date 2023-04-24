using Microsoft.AspNetCore.Http;

namespace com.softpine.muvany.infrastructure.Identity.Auth;

/// <summary>
/// 
/// </summary>
public class CurrentUserMiddleware : IMiddleware
{
    private readonly ICurrentUserInitializer _currentUserInitializer;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="currentUserInitializer"></param>
    public CurrentUserMiddleware(ICurrentUserInitializer currentUserInitializer) =>
        _currentUserInitializer = currentUserInitializer;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        _currentUserInitializer.SetCurrentUser(context.User);

        await next(context);
    }
}
