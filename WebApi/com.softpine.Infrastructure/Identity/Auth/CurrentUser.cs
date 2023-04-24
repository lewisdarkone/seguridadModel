using System.Security.Claims;
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.models.Constants;
using com.softpine.muvany.models.Interfaces;

namespace com.softpine.muvany.infrastructure.Identity.Auth;

/// <summary>
/// 
/// </summary>
public class CurrentUser : ICurrentUser, ICurrentUserInitializer
{
    private ClaimsPrincipal? _user;

    /// <summary>
    /// 
    /// </summary>
    public string? Name => _user?.Identity?.Name;

    private string _userId = String.Empty;
    private readonly IUserService _userService; 
    /// <summary>
    /// Constructor para inyectar los objetos
    /// </summary>
    /// <param name="userService"></param>
    //public CurrentUser(IUserService userService)
    //{
    //    _userService = userService;  
    //}
    public string GetUserId() =>
        IsAuthenticated()
            ? (_user?.GetUserId() ?? string.Empty)
            : _userId;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public string? GetUserEmail() =>
        IsAuthenticated()
            ? _user!.GetEmail()
            : string.Empty;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public string? GetUserName() =>
        IsAuthenticated()
            ? _user!.GetFullName()
            : string.Empty;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool IsAuthenticated() =>
        _user?.Identity?.IsAuthenticated is true;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public bool IsInRole(string role) =>
        _user?.IsInRole(role) is true;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Claim>? GetUserClaims() =>
        _user?.Claims;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <exception cref="Exception"></exception>
    public void SetCurrentUser(ClaimsPrincipal user)
    {
        if (_user != null)
        {
            throw new Exception(ApiConstants.Messages.InScopeInitializationReservedError);
        }

        _user = user;
    }
      
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <exception cref="Exception"></exception>
    public void SetCurrentUserId(string userId)
    {
        if (_userId != string.Empty)
        {
            throw new Exception("Method reserved for in-scope initialization");
        }

        if (!string.IsNullOrEmpty(userId))
        {
            _userId = userId;
        }
    }
}
