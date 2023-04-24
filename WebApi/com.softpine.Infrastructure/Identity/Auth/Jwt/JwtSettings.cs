namespace com.softpine.muvany.infrastructure.Identity.Auth.Jwt;

/// <summary>
/// 
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// 
    /// </summary>
    public string? Key { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int TokenExpirationInMinutes { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int RefreshTokenExpirationInDays { get; set; }
}
