using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Matrices.Api.Responses;
using com.softpine.muvany.core.Identity;
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.infrastructure.Shared.Middleware;
using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.DTOS;

namespace com.softpine.muvany.WebApi.Controllers.Identity;
/// <summary>
/// Matenimiento para los procesos relacionados con el token.
/// </summary>
public sealed class TokensController : VersionNeutralApiController
{
    private readonly ITokenService _tokenService;

    /// <summary>
    /// Constructor para la inyección de interfaces
    /// </summary>
    /// <param name="tokenService"></param>
    public TokensController(ITokenService tokenService) => _tokenService = tokenService;

    /// <summary>
    /// Retorna el token de acceso y datos del usuario.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    [OpenApiOperation("Retorna el token de acceso y datos del usuario", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<TokenResponse>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> GetTokenAsync(TokenRequest request)
    {        
       var response  = await _tokenService.GetTokenAsync(request, GetIpAddress());
       return Ok(new ApiResponse<TokenResponse>(response));
    }

    /// <summary>
    /// Retorna el token de acceso a usuarios con el email y codigo de validacion enviado por correo o sms
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("validateCode")]
    [AllowAnonymous]
    [OpenApiOperation("Retorna el token de acceso y datos del usuario", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<TokenResponse>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> GetTokenByCodeAsync(TokenByCodeRequest request)
    {
        var response = await _tokenService.GetTokenByValidateCode(request, GetIpAddress());
        return Ok(new ApiResponse<TokenResponse>(response));
    }

    /// <summary>
    /// Retorna una actualizacion del token de acceso del usuario.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("refresh")]
    [AllowAnonymous]
    [OpenApiOperation("Retorna una actualizacion del token de acceso del usuario", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<TokenResponse>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> RefreshAsync(RefreshTokenRequest request)
    {
        var response = await _tokenService.RefreshTokenAsync(request, GetIpAddress());
        return Ok(new ApiResponse<TokenResponse>(response));
    }

    private string GetIpAddress() =>
        Request.Headers.ContainsKey("X-Forwarded-For")
            ? Request.Headers["X-Forwarded-For"]
            : HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "N/A";
}
