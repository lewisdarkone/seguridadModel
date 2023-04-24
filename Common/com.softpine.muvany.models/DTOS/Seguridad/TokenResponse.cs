namespace com.softpine.muvany.models.DTOS;

/// <summary>
/// 
/// </summary>
/// <param name="Token"></param>
/// <param name="RefreshToken"></param>
/// <param name="RefreshTokenExpiryTime"></param>
/// <param name="TokenExpiryTime"></param>
/// <param name="Email"></param>
/// <param name="NombreCompleto"></param>
/// <param name="TypeRol"></param>
/// <param name="Role"></param>
public record TokenResponse(string Token, string RefreshToken, DateTime? RefreshTokenExpiryTime, DateTime? TokenExpiryTime, string Email, string NombreCompleto, int TypeRol, IList<string> Role);
