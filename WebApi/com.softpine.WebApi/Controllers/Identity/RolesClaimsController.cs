using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NSwag.Annotations;
using Matrices.Api.Responses;
using com.softpine.muvany.infrastructure.Identity.Auth.Permissions;
using com.softpine.muvany.infrastructure.Identity.Services;
using com.softpine.muvany.infrastructure.Shared.Middleware;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.QueryFilters;
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.Requests;

namespace com.softpine.muvany.WebApi.Controllers;
/// <summary>
/// Proceso para el mantenimiento de los permisos de los Roles
/// </summary>
public class RolesClaimsController : VersionNeutralApiController
{
    private readonly IRolesClaimService _rolesClaimService;
    private readonly IUriService _uriService;
    /// <summary>
    /// Constructor para la inyección de interfaces
    /// </summary>
    /// <param name="rolesClaimService"></param>
    /// <param name="uriService"></param>
    public RolesClaimsController(IRolesClaimService rolesClaimService, IUriService uriService)
    {
        _rolesClaimService = rolesClaimService;
        _uriService = uriService;
    }

    /// <summary>
    /// Retorna los permisos de un rol.
    /// </summary>
    /// <param name="filters"></param>
    /// <returns></returns>
    [HttpGet("get", Name = "GetRolesPermissionsAsync")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<ApplicationRoleClaimDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    [OpenApiOperation("Retorna todos los registros de los roles claim.", "")]
    public async Task<IActionResult> GetRolesPermissionsAsync([FromQuery] ApplicationRoleClaimQueryFilter filters)
    {
        var response = await _rolesClaimService.GetRolesClaimAsync(filters);
        var Metadata = new Metadata
        {
            TotalCount = response.TotalCount,
            PageSize = response.PageSize,
            CurrentPage = response.CurrentPage,
            TotalPages = response.TotalPages,
            HasNextPage = response.HasNextPage,
            HasPreviousPage = response.HasPreviousPage,
            NextPageUrl = _uriService.GetPaginationUri(filters, Url.RouteUrl(nameof(GetRolesPermissionsAsync))).ToString(),
            PreviousPageUrl = _uriService.GetPaginationUri(filters, Url.RouteUrl(nameof(GetRolesPermissionsAsync))).ToString()
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(Metadata));
        return Ok(new ApiResponse<IEnumerable<ApplicationRoleClaimDto>>(response) { Meta = Metadata });
    }

    /// <summary>
    /// Retorna los permisos de un rol.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get/{id}")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<List<RolesClaimDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    [OpenApiOperation("Retorna los permisos de un rol.", "")]
    public async Task<IActionResult> GetRoleByIdWithPermissionsAsync(string id)
    {
        var response = await _rolesClaimService.GetRolByIdWithPermissionsAsync(id);
        return Ok(new ApiResponse<RolesClaimDto>(response));
    }

    /// <summary>
    /// Actualizar los permisos de un rol.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("assing-permisos")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    [OpenApiOperation("Agregar los permisos de un rol.", "")]
    public async Task<IActionResult> AddRolePermissionsAsync(CreateRolePermissionsRequest request)
    {
        var response = await _rolesClaimService.AddPermissionsRolAsync(request);
        return Ok(new ApiResponse<bool>(response));
    }

    /// <summary>
    /// Actualizar los permisos de un rol.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("update")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    [OpenApiOperation("Actualizar los permisos de un rol.", "")]
    public async Task<IActionResult> UpdateRolePermissionsAsync(UpdateRolePermissionsRequest request)
    {
        var response = await _rolesClaimService.UpdatePermissionsRolAsync(request);
        return Ok(new ApiResponse<bool>(response));
    }

    /// <summary>
    /// Actualizar los permisos de un rol.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpDelete("delete")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    [OpenApiOperation("Eliminar los permisos de un rol.", "")]
    public async Task<IActionResult> DeleteRolePermissionsAsync(DeleteRolePermissionsRequest request)
    {
        var response = await _rolesClaimService.DeletePermissionsRolAsync(request);
        return Ok(new ApiResponse<bool>(response));
    }

}
