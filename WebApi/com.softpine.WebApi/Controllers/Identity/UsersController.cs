using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NSwag.Annotations;
using Matrices.Api.Responses;
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.infrastructure.Identity.Auth.Permissions;
using com.softpine.muvany.infrastructure.Shared.Middleware;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.QueryFilters;
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.Requests;

namespace com.softpine.muvany.WebApi.Controllers.Identity;
/// <summary>
/// Proceso para el mantenimiento de Usuarios.
/// </summary>
/// 
public class UsersController : VersionNeutralApiController
{
    private readonly IUserService _userService;
    private readonly IUriService _uriService;
    private readonly IConfiguration _config;

    /// <summary>
    /// Constructor para la inyección de interfaces.
    /// </summary>
    /// <param name="userService"></param>
    /// <param name="uriService"></param>
    public UsersController(IUserService userService, IUriService uriService, IConfiguration config)
    {
        _userService = userService;
        _uriService = uriService;
        _config = config;
    }

    /// <summary>
    /// Retorna la lista de todos los usuarios registrados.
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [OpenApiOperation("Retorna la lista de todos los usuarios registrados.", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<UserDetailsDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    [HttpGet("get", Name = nameof(GetUsersAsync))]
    public async Task<IActionResult> GetUsersAsync([FromQuery] UserQueryFilter filter)
    {

        var response = await _userService.GetListAsync(filter);
        var resultMeta = new Metadata
        {
            TotalCount = response.TotalCount,
            PageSize = response.PageSize,
            CurrentPage = response.CurrentPage,
            TotalPages = response.TotalPages,
            HasNextPage = response.HasNextPage,
            HasPreviousPage = response.HasPreviousPage,
            NextPageUrl = _uriService.GetPaginationUri(filter, Url.RouteUrl(nameof(GetUsersAsync))).ToString()
        };
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(resultMeta));
        return Ok(new ApiResponse<IEnumerable<UserDetailsDto>>(response) { Meta = resultMeta });
    }

    /// <summary>
    /// Retorna los datos de un usuario registrado.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [OpenApiOperation("Retorna los datos de un usuario registrado", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<UserDetailsDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetUserByIdAsync(string id)
    {
        var response = await _userService.GetUserByIdAsync(id);
        return Ok(new ApiResponse<UserDetailsDto>(response));
    }



    /// <summary>
    /// Retorna los roles de un usuario.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [OpenApiOperation("Retorna los roles de un usuario.", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<List<UserRoleDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    [HttpGet("get/{id}/roles")]
    public async Task<IActionResult> GetRolesUserAsync(string id)
    {
        var response = await _userService.GetRolesByUserIdAsync(id);
        return Ok(new ApiResponse<List<UserRoleDto>>(response));
    }

    /// <summary>
    /// Asignación de roles de usuario.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [OpenApiOperation("Asignación de roles de usuario.", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    [HttpPut("update/{id}/roles")]
    public async Task<IActionResult> AssignRolesUserAsync(string id, UserRolesRequest request)
    {
        var response = await _userService.AssignRolesAsync(id, request);
        return Ok(new ApiResponse<bool>(response));
    }

    /// <summary>
    /// Creación de un nuevo usuario.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [OpenApiOperation("Creación de un nuevo usuario.", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    [HttpPost("create")]
    public async Task<IActionResult> CreateUserAsync(CreateUserRequest request)
    {

        var serverApp = _config.GetSection($"MailSettings:RedirectServer").Value;
        var response = await _userService.CreateUserAsync(request, serverApp);
        return Ok(new ApiResponse<bool>(response));
    }

    /// <summary>
    /// Registrar un usuario nuevo via formulario
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    //[Authorization("Authorization")]
    [AllowAnonymous]
    [OpenApiOperation("Registrar un usuario nuevo via formulario", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserFormAsync(RegisterUserRequest request)
    {

        var serverApp = _config.GetSection($"MailSettings:RedirectServer").Value;
        var response = await _userService.RegisterUserAsync(request, serverApp);
        return Ok(new ApiResponse<bool>(response));
    }

    /// <summary>
    /// Actualiza el estatus de un usuario registrado.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    [OpenApiOperation("Actualiza el estatus de un usuario registrado.", "")]
    [HttpPost("change-status")]
    public async Task<ActionResult> EnableDisableUserAsync(ToggleUserStatusRequest request)
    {
        var response = await _userService.ChangeStatusAsync(request);
        return Ok(new ApiResponse<bool>(response));
    }
    /// <summary>
    /// Actualiza un usuario registrado.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    [OpenApiOperation("Actualiza un usuario registrado.", "")]
    [HttpPut("update")]
    public async Task<ActionResult> UpdateUserAsync(UpdateUserRequest request)
    {
        var serverApp = _config.GetSection($"MailSettings:RedirectServer").Value;
        var response = await _userService.UpdateAsync(request, serverApp);
        return Ok(new ApiResponse<bool>(response));
    }

    /// <summary>
    /// Confirmación de correo a usuario registrado.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="tenant"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    //[AllowAnonymous]
    //[OpenApiOperation("Confirmación de correo a usuario registrado", "")]
    //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    //[HttpGet("confirm-email")]
    //public async Task<ActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string? tenant, [FromQuery] string? code)
    //{
    //    var response = await _userService.ConfirmEmailAsync(userId, code, tenant);
    //    return Ok(new ApiResponse<bool>(response));
    //}

    //[AllowAnonymous]
    //[OpenApiOperation("Confirm phone number for a user.", "")]
    //[ApiConventionMethod(typeof(MuvanyApiConventions), nameof(MuvanyApiConventions.Search))]
    //[HttpGet("confirm-phone-number")]
    //public Task<string> ConfirmPhoneNumberAsync([FromQuery] string userId, [FromQuery] string code)
    //{
    //    return _userService.ConfirmPhoneNumberAsync(userId, code);
    //}

    /// <summary>
    /// Restauración de contraseña olvidada.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [OpenApiOperation("Restauración de contraseña olvidada.", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<ResetPasswordRequest>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    [HttpPost("forgot-password")]
    public async Task<ActionResult> ForgotPasswordAsync([FromQuery] ForgotPasswordRequest request)
    {
        var serverApp = _config.GetSection($"MailSettings:RedirectServer").Value;
        return Ok(new ApiResponse<bool>(await _userService.ForgotPasswordAsync(request, serverApp)));
    }

    /// <summary>
    /// Reseteando la contraseña de un usuario.  y Confirmacion de email usuario nuevo
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [OpenApiOperation("Reseteando la contraseña de un usuario.", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    [HttpPost("reset-password")]
    public async Task<ActionResult> ResetPasswordAsync(ResetPasswordRequest request)
    {
        return Ok(new ApiResponse<bool>(await _userService.ResetPasswordAsync(request)));
    }

    private string GetOriginFromRequest() => $"{Request.Scheme}://{Request.Host.Value}{Request.PathBase.Value}";
}
