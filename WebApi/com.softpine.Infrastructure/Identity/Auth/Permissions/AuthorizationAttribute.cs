using com.softpine.muvany.core.Authorization;
using Microsoft.AspNetCore.Authorization;


namespace com.softpine.muvany.infrastructure.Identity.Auth.Permissions;

/// <summary>
/// 
/// </summary>
public class AuthorizationAttribute : AuthorizeAttribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    public AuthorizationAttribute(string type) =>
        Policy = type;
}
