using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NSwag.Annotations;
using Matrices.Api.Responses;
using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.infrastructure.Identity.Auth.Permissions;
using com.softpine.muvany.infrastructure.Shared.Middleware;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.QueryFilters;
using com.softpine.muvany.models.Enumerations;
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.Requests;

namespace com.softpine.muvany.WebApi.Controllers;
/// <summary>
/// Proceso para el mantenimiento de Roles
/// </summary>
public class RolesController : VersionNeutralApiController
{
    private readonly IRoleService _roleService;
    private readonly IUriService _uriService;
    private readonly IUserService _userService;
    /// <summary>
    /// Constructor para la inyección de interfaces
    /// </summary>
    /// <param name="roleService"></param>
    /// <param name="uriService"></param> 
    public RolesController(IRoleService roleService, IUriService uriService, IUserService userService)
    {
        _roleService = roleService;
        _uriService = uriService;
        _userService = userService;
    }

    /// <summary>
    /// Retorna una lista de los roles.
    /// </summary>
    [HttpGet("get", Name = "GetListRolAsync")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<RoleDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    [OpenApiOperation("Retorna una lista de los roles.", "")]
    public async Task<IActionResult> GetListRolAsync([FromQuery] RolesQueryFilter filters)
    {
        if ( !_userService.ValidateUserInterAsync(_userService.GetUserLoginId().Result).Result )
            filters.TypeRol = (int)ParametrosGeneralesEnum.Externo;
        var response = await _roleService.GetListAsync(filters);
        
        var Metadata = new Metadata
        {
            TotalCount = response.TotalCount,
            PageSize = response.PageSize,
            CurrentPage = response.CurrentPage,
            TotalPages = response.TotalPages,
            HasNextPage = response.HasNextPage,
            HasPreviousPage = response.HasPreviousPage,
            NextPageUrl = _uriService.GetPaginationUri(filters, Url.RouteUrl(nameof(GetListRolAsync))).ToString(),
            PreviousPageUrl = _uriService.GetPaginationUri(filters, Url.RouteUrl(nameof(GetListRolAsync))).ToString()
        };
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(Metadata));
        return Ok(new ApiResponse<IEnumerable<RoleDto>>(response) { Meta = Metadata });
    }

    /// <summary>
    /// Retorna los datos de un rol especifico.
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("get/{id}")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<RoleDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    [OpenApiOperation("Retorna los datos de un rol especifico.", "")]
    public async Task<IActionResult> GetRolByIdAsync(string id)
    {
        var response = await _roleService.GetByIdAsync(id);
        return Ok(new ApiResponse<RoleDto>(response));
    }

    /// <summary>
    /// Crear ó actualizar un rol.
    /// </summary>
    /// <param name="request"></param>
    [HttpPost("create")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    [OpenApiOperation("Crear un rol.", "")]
    public async Task<IActionResult> RegisterRoleAsync(CreateRoleRequest request)
    {
        var response = await _roleService.CreateAsync(request);
        return Ok(new ApiResponse<bool>(response));
    }

    /// <summary>
    /// Crear ó actualizar un rol.
    /// </summary>
    /// <param name="request"></param>
    [HttpPut("update")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    [OpenApiOperation("Actualizar un rol.", "")]
    public async Task<IActionResult> UpdateRoleAsync(UpdateRoleRequest request)
    {
        var response = await _roleService.UpdateAsync(request);
        return Ok(new ApiResponse<bool>(response));
    }

    /// <summary>
    /// Eliminar un rol.
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("delete/{id}")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    [OpenApiOperation("Eliminar un rol.", "")]
    public async Task<IActionResult> DeleteRoleAsync(string id)
    {
        var response = await _roleService.DeleteAsync(id);
        return Ok(new ApiResponse<bool>(response));
    }

    /// <summary>
    /// Eliminar un rol.
    /// </summary>
    /// <param name="filter"></param>
    //[HttpGet("get/TiposRoles")]
    //[Authorization("Authorization")]
    //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<ParametrosGeneralesDto>>))]
    //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    //[OpenApiOperation("Obtener los diferentes tipos de Roles.", "")]
    //public async Task<IActionResult> GetTypesRolesAsync([FromQuery] ParametrosGeneralesQueryFilter filter)
    //{
    //    var response = await _parametrosGeneralesService.GetParametrosGeneralesByIdCatalogo((int)CatalogoParametrosEnum.TipoRol, filter);
    //    return Ok(new ApiResponse<IEnumerable<ParametrosGeneralesDto>>(response));
    //}

}
