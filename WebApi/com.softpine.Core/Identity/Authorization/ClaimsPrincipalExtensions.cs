using com.softpine.muvany.core.Authorization;

namespace System.Security.Claims;

/// <summary>
/// 
/// </summary>
public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="principal"></param>
    /// <returns></returns>
    public static string? GetEmail(this ClaimsPrincipal principal)
        => principal.FindFirstValue(ClaimTypes.Email);

   /// <summary>
   /// 
   /// </summary>
   /// <param name="principal"></param>
   /// <returns></returns>
   public static string? GetFullName(this ClaimsPrincipal principal)
        => principal?.FindFirst(MuvanyClaims.Fullname)?.Value;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="principal"></param>
    /// <returns></returns>
    public static string? GetFirstName(this ClaimsPrincipal principal)
        => principal?.FindFirst(ClaimTypes.Name)?.Value;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="principal"></param>
    /// <returns></returns>
    public static string? GetSurname(this ClaimsPrincipal principal)
        => principal?.FindFirst(ClaimTypes.Surname)?.Value;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="principal"></param>
    /// <returns></returns>
    public static string? GetPhoneNumber(this ClaimsPrincipal principal)
        => principal.FindFirstValue(ClaimTypes.MobilePhone);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="principal"></param>
    /// <returns></returns>
    public static string? GetUserId(this ClaimsPrincipal principal)
       => principal.FindFirstValue(ClaimTypes.NameIdentifier);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="principal"></param>
    /// <returns></returns>
    public static string? GetImageUrl(this ClaimsPrincipal principal)
       => principal.FindFirstValue(MuvanyClaims.ImageUrl);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="principal"></param>
    /// <returns></returns>
    public static DateTimeOffset GetExpiration(this ClaimsPrincipal principal) =>
        DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(
            principal.FindFirstValue(MuvanyClaims.Expiration)));

    private static string? FindFirstValue(this ClaimsPrincipal principal, string claimType) =>
        principal is null
            ? throw new ArgumentNullException(nameof(principal))
            : principal.FindFirst(claimType)?.Value;
}
