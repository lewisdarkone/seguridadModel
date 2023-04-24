using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.infrastructure.Shared.Middleware;
using com.softpine.muvany.core.Exceptions;
using com.softpine.muvany.infrastructure.Identity.Services;
using Matrices.Api.Responses;
using com.softpine.muvany.WebApi.Host.Controllers;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.Requests;

namespace com.softpine.muvany.WebApi.Controllers;
/// <summary>
/// Proceso para el mantenimiento de Perfiles de usuario
/// </summary>
public class PersonalController : VersionNeutralApiController
{
    private readonly IUserService _userService;
    private readonly IRolesClaimService _rolesClaimService;
    /// <summary>
    /// Controlador para la inyeccion de las dependencias
    /// </summary>
    /// <param name="userService"></param>
    /// <param name="rolesClaimService"></param>
    public PersonalController(IUserService userService, IRolesClaimService rolesClaimService)
    {
        _userService = userService;
        _rolesClaimService = rolesClaimService;
    }

    /// <summary>
    /// Retorna el perfil de usuario actualmente logeado
    /// </summary>
    /// <param name="cancellationToken"></param>
    //[Authorization("Authorization")]
    [HttpGet("profile")]
    [OpenApiOperation("Retorna el perfil de usuario actualmente logeado.", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<UserDetailsDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    public async Task<ActionResult<UserDetailsDto>> GetProfileAsync(CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }
        var user = await _userService.GetUserByIdAsync(userId);

        return Ok(new ApiResponse<UserDetailsDto>(user));
    }

    /// <summary>
    /// Actualización del perfil del usuario
    /// </summary>
    /// <param name="request"></param>
    //[Authorization("Authorization")]
    [HttpPut("profile")]
    [OpenApiOperation("Actualización del perfil del usuario.", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    public async Task<ActionResult> UpdateProfileAsync(UpdateUserProfileRequest request)
    {
        if (User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }
        var userUpdarte = new UpdateUserRequest { Id = request.Id, PhoneNumber = request.PhoneNumber,TwoFactorEnable = request.TwoFactorEnable };
        var response = await _userService.UpdateAsync(userUpdarte);
        return Ok(new ApiResponse<bool>(response));
    }

    /// <summary>
    /// Cambio de contraseña del usuario
    /// </summary>
    /// <param name="model"></param>
    //[Authorization("Authorization")]
    [HttpPut("change-password")]
    [OpenApiOperation("Cambio de contraseña del usuario.", "")]
    [ApiConventionMethod(typeof(MuvanyApiConventions), nameof(MuvanyApiConventions.Register))]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    public async Task<ActionResult> ChangePasswordAsync(ChangePasswordRequest model)
    {
        if (User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var response = await _userService.ChangePasswordAsync(model, userId);
        return Ok(new ApiResponse<bool>(response));
    }

    /// <summary>
    /// Retorna los permisos del usuario logeado
    /// </summary>
    //[Authorization("Authorization")]
    [HttpGet("permissions")]
    [OpenApiOperation("Retorna los permisos del usuario logueado.", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<List<RolesClaimDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> GetPermissionsAsync()
    {
        var valUserId = User.GetUserId();
        var RoleClaims = await _userService.GetRolesByUserIdAsync(valUserId);
        var dataPermissions = new List<RolesClaimDto>();
        foreach (var item in RoleClaims)
        {
            var responsePermissions = await _rolesClaimService.GetRolByIdWithPermissionsAsync(item.RoleId);
            if (responsePermissions == null)
                throw new CustomException("No hay permisos asociados a esta descripción");
            dataPermissions.Add(responsePermissions);
        }

        return Ok(new ApiResponse<List<RolesClaimDto>>(dataPermissions));
    }


}
