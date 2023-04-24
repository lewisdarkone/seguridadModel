using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using com.softpine.muvany.core.Authorization;
using com.softpine.muvany.core.Exceptions;
using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.core.Requests;
using com.softpine.muvany.infrastructure.Identity.Entities;
using com.softpine.muvany.core.Identity;
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.infrastructure.Identity.Auth;
using com.softpine.muvany.infrastructure.Identity.Auth.Jwt;
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.Constants;
using com.softpine.muvany.models.Enumerations;
using com.softpine.muvany.models.DTOS;

namespace com.softpine.muvany.infrastructure.Identity.Services;

internal class TokenService : ITokenService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SecuritySettings _securitySettings;
    private readonly JwtSettings _jwtSettings;
    private readonly ConfigurationsConstants _configurationsConstants;
    private readonly IUserService _userService;
    private readonly IMailService _mailService;
    private readonly IUsersDomainService _userDomainService;

    public TokenService(
        UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> jwtSettings,
        IOptions<SecuritySettings> securitySettings,
        IOptions<ConfigurationsConstants> configurationsConstants
        , IUserService userService
        , IUsersDomainService userDomainService
        , IMailService mailService        )
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
        _securitySettings = securitySettings.Value;
        _configurationsConstants = configurationsConstants.Value;
        _userService = userService;
        _userDomainService = userDomainService;
        _mailService = mailService;
    }

    public async Task<TokenResponse> GetTokenAsync(TokenRequest request, string ipAddress)
    {
        var user = await _userManager.FindByEmailAsync(request.Email.Trim().Normalize());

        if ( user == null )
            throw new BusinessException(ApiConstants.Messages.UserNotFound);

        //validar fecha de expiración de la cuenta
        if (user.ExpirePasswordDate != null && user.ExpirePasswordDate.Value <= DateTime.Now)
        {
            throw new UnauthorizedException(ApiConstants.Messages.ExpiredAccount);
        }

        if (user.Estado != 1)
        {
            throw new UnauthorizedException(ApiConstants.Messages.UserNotActive);
        }

        if (_securitySettings.RequireConfirmedAccount && !user.EmailConfirmed)
        {
            throw new UnauthorizedException(ApiConstants.Messages.EmailNotConfirmed);
        }

        

        var valPassword = request.Password;

        if ( request.Email.Contains(_configurationsConstants.EmailInternoFirma, StringComparison.OrdinalIgnoreCase) )
        {

            var isValidUserDomain = false;
            var roles = await _userService.GetRolesByUserIdAsync(user.Id);
            if ( !roles.Any(p => p.RoleName.Equals(nameof(RoleType.AplicacionesInternas))) && !_configurationsConstants.ServerDevelopment.Equals("true") )
            {
                isValidUserDomain = await _userDomainService.ValidateCredentials(request.Email, request.Password);
                if ( isValidUserDomain )
                    valPassword = _configurationsConstants.PasswordUserInterInitial;
                else
                    throw new BusinessException(ApiConstants.Messages.UserNotValid);
            }
        }

        //si la clave es incorrecta entoces aumentar el conteo, si el conteo llega a n entonces bloquear.
        if ( !await _userManager.CheckPasswordAsync(user, valPassword) )
        {
            if(!request.Email.Contains(_configurationsConstants.EmailInternoFirma, StringComparison.OrdinalIgnoreCase))
                await _userService.AddFailLoggin(user.Id);

            throw new UnauthorizedException(ApiConstants.Messages.UserNotValid);
        }

        if ( user.TwoFactorEnabled )
        {
            var newCode = await _userService.ResetTempCode(user.Id);
            
            await _mailService.SendEmailCode("lewis.burgos@outlook.com", newCode,"Lewis Rodriguez");  
            return await GenerateTokensAndUpdateUser(user, ipAddress, true);

        }

        return await GenerateTokensAndUpdateUser(user, ipAddress, false);
    }


    public async Task<TokenResponse> GetTokenByValidateCode(TokenByCodeRequest request, string ipAddress)
    {
        var user = await _userManager.FindByEmailAsync(request.Email.Trim().Normalize());

        if ( user == null )
            throw new BusinessException(ApiConstants.Messages.UserNotFound);

        var validCode = await _userService.ValidateCodeIsValid(user.Id,request.Code);

        if(validCode)
            return await GenerateTokensAndUpdateUser(user,ipAddress, false);

        throw new ArgumentException(ApiConstants.Messages.NotValidParameter);
    }
    public async Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress)
    {
        var userPrincipal = GetPrincipalFromExpiredToken(request.Token);
        string? userEmail = userPrincipal.GetEmail();
        var user = await _userManager.FindByEmailAsync(userEmail);
        if ( user is null )
        {
            throw new UnauthorizedException(ApiConstants.Messages.AuthenticationFailed);
        }

        if ( user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow )
        {
            throw new UnauthorizedException(ApiConstants.Messages.ExpiredToken);
        }

        return await GenerateTokensAndUpdateUser(user, ipAddress);
    }

    private async Task<TokenResponse> GenerateTokensAndUpdateUser(ApplicationUser user, string ipAddress, bool TwoFactor = false)
    {
        string token = "";
        
        if(!TwoFactor)
            token = GenerateJwt(user, ipAddress);

        user.RefreshToken = GenerateRefreshToken();
        var TokenExpiryTime = DateTime.Now.AddMinutes(_jwtSettings.TokenExpirationInMinutes);
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpirationInDays);
        //  var TokenExpiryTime = DateTime.Now.AddMinutes(1);

        await _userManager.UpdateAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        var typerol = _userService.GetRolesByUserIdAsync(user.Id).Result.FirstOrDefault();
        return new TokenResponse(token, user.RefreshToken, user.RefreshTokenExpiryTime, TokenExpiryTime, user.Email, user.NombreCompleto, (int)typerol.TypeRol, roles);
    }

    private string GenerateJwt(ApplicationUser user, string ipAddress) =>
        GenerateEncryptedToken(GetSigningCredentials(), GetClaims(user, ipAddress));

    private IEnumerable<Claim> GetClaims(ApplicationUser user, string ipAddress) =>
        new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email),
            new(MuvanyClaims.Fullname, $"{user.NombreCompleto}"),
            new(MuvanyClaims.IpAddress, ipAddress),
            new(MuvanyClaims.ImageUrl, user.ImagenUrl ?? string.Empty),
            new(ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty)
        };

    private string GenerateRefreshToken()
    {
        byte[] randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private string GenerateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
    {
        var token = new JwtSecurityToken(
           claims: claims,
           expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes),
           signingCredentials: signingCredentials);
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        if ( string.IsNullOrEmpty(_jwtSettings.Key) )
        {
            throw new InvalidOperationException("No Key defined in JwtSettings config.");
        }

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
            ValidateIssuer = false,
            ValidateAudience = false,
            RoleClaimType = ClaimTypes.Role,
            ClockSkew = TimeSpan.Zero,
            ValidateLifetime = false
        };
        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if ( securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase) )
        {
            throw new UnauthorizedException(ApiConstants.Messages.InvalidToken);
        }

        return principal;
    }

    private SigningCredentials GetSigningCredentials()
    {
        if ( string.IsNullOrEmpty(_jwtSettings.Key) )
        {
            throw new InvalidOperationException(ApiConstants.Messages.NotDefinedTokenKey);
        }

        byte[] secret = Encoding.UTF8.GetBytes(_jwtSettings.Key);
        return new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);
    }

    
}
