using com.softpine.muvany.core.Identity;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.Requests;

namespace com.softpine.muvany.core.Interfaces;

/// <summary>
/// 
/// </summary>
public interface ITokenService : ITransientService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ipAddress"></param>
    /// <returns></returns>
    Task<TokenResponse> GetTokenAsync(TokenRequest request, string ipAddress);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ipAddress"></param>
    /// <returns></returns>
    Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress);

    /// <summary>
    /// Obtener token por medio de codigo enviado por correo o sms
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ipAddress"></param>
    /// <returns></returns>
    Task<TokenResponse> GetTokenByValidateCode(TokenByCodeRequest request, string ipAddress);
}
