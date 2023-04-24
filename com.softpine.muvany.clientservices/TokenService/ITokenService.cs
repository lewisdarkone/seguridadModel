

using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.ResponseModels.TokenResponse;

namespace com.softpine.muvany.clientservices;

public interface ITokenService
{
    Task<GetTokenResponse?> Login(TokenRequest getTokenResquest);
    Task<GetTokenResponse?> RefreshToken(RefreshTokenRequest getTokenResquest);
    Task<GetTokenResponse?> GetByCodeToken(TokenByCodeRequest getTokenResquest);

}
