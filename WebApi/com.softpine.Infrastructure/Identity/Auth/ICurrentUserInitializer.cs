using System.Security.Claims;

namespace com.softpine.muvany.infrastructure.Identity.Auth;

/// <summary>
/// 
/// </summary>
public interface ICurrentUserInitializer
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    void SetCurrentUser(ClaimsPrincipal user);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    void SetCurrentUserId(string userId);
}
