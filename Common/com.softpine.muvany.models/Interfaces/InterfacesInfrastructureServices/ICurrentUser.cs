using System.Security.Claims;

namespace com.softpine.muvany.models.Interfaces;

/// <summary>
/// 
/// </summary>
public interface ICurrentUser
{
    /// <summary>
    /// 
    /// </summary>
    string? Name { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    string GetUserId();

    

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    string? GetUserEmail();

    /// <summary>
    /// Obtener el username del usuario logueado
    /// </summary>
    /// <returns></returns>
    string? GetUserName();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    bool IsAuthenticated();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    bool IsInRole(string role);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerable<Claim>? GetUserClaims();

}
