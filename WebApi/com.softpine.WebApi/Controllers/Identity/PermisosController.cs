using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NSwag.Annotations;
using Matrices.Api.Responses;
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.infrastructure.Identity.Auth.Permissions;
using com.softpine.muvany.infrastructure.Shared.Middleware;
using Microsoft.AspNetCore.Authorization;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.QueryFilters;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.Requests;

namespace com.softpine.muvany.WebApi.Controllers;
/// <summary>
/// Procesos para el mantenimiento de Permisos.
/// </summary>
public class PermisosController : VersionNeutralApiController
{
    private readonly IPermisosService _permisosService;
    private readonly IUriService _uriService;

    /// <summary>
    /// Constructor para la inyección de las interfaces.
    /// </summary>
    /// <param name="permisosService"></param>
    /// <param name="uriService"></param>
    public PermisosController(IPermisosService permisosService, IUriService uriService)
    {
        _permisosService = permisosService;
        _uriService = uriService;
    }

    /// <summary>
    /// Retorna una lista de todos los permisos.
    /// </summary>
    /// <param name="filters"></param>
    /// <returns></returns>
    [HttpGet("get", Name = "GetListPermisosAsync")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [OpenApiOperation("Retorna una lista de todos los permisos", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<PermisosDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> GetListPermisosAsync([FromQuery] PermisosQueryFilter filters)
    {
        var permisos = await _permisosService.GetPermisos(filters);

        var Metadata = new Metadata
        {
            TotalCount = permisos.TotalCount,
            PageSize = permisos.PageSize,
            CurrentPage = permisos.CurrentPage,
            TotalPages = permisos.TotalPages,
            HasNextPage = permisos.HasNextPage,
            HasPreviousPage = permisos.HasPreviousPage,
            NextPageUrl = _uriService.GetPaginationUri(filters, Url.RouteUrl(nameof(GetListPermisosAsync))).ToString(),
            PreviousPageUrl = _uriService.GetPaginationUri(filters, Url.RouteUrl(nameof(GetListPermisosAsync))).ToString()
        };

        var response = new ApiResponse<IEnumerable<PermisosDto>>(permisos)
        {
            Meta = Metadata
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(Metadata));

        return Ok(response);
    }

    /// <summary>
    /// Registra un permiso
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("create")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [OpenApiOperation("Registra un permiso", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<PermisosDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> CreatePermisoAsync(CreateOrUpdatePermisoRequest request)
    {
        var permisoDto = await _permisosService.InsertPermiso(request);
        var response = new ApiResponse<PermisosDto>(permisoDto);
        return Ok(response);
    }

    /// <summary>
    /// Actualiza un permiso
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("update")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [OpenApiOperation("Actualiza un permiso", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> UpdatePermisoAsync(CreateOrUpdatePermisoRequest request)
    {
        var result = await _permisosService.UpdatePermiso(request);
        var response = new ApiResponse<bool>(result);
        return Ok(response);
    }

    /// <summary>
    /// Elimina un permiso
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    //[HttpDelete("delete/{id}")]
    ////[Authorization("Authorization")]
    //[AllowAnonymous]
    //[OpenApiOperation("Elimina un permiso", "")]
    //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    //public async Task<IActionResult> DeletePermisoAsync(int id)
    //{
    //    var result = await _permisosService.DeletePermiso(id);
    //    var response = new ApiResponse<bool>(result);
    //    return Ok(response);
    //}


}
