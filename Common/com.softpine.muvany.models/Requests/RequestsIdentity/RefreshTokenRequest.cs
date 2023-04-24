namespace com.softpine.muvany.models.Requests;

/// <summary>
/// 
/// </summary>
/// <param name="Token"></param>
/// <param name="RefreshToken"></param>
public record RefreshTokenRequest(string Token, string RefreshToken);
