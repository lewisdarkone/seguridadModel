using System.Net;
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

namespace com.softpine.muvany.WebApi.Controllers;

/// <summary>
/// Procesos de los mantenimientos de endpoints 
/// </summary>
public class EndpointsController : VersionNeutralApiController
{
    private readonly IEndpointsService _endpointsService;
    private readonly IUriService _uriService;

    /// <summary>
    /// Contructor para la inyección de las interfaces
    /// </summary>
    /// <param name="endpointsService"></param>
    /// <param name="uriService"></param>
    public EndpointsController(IEndpointsService endpointsService, IUriService uriService)
    {

        _endpointsService = endpointsService;
        _uriService = uriService;

    }

    /// <summary>
    /// Retorna una lista de todos los endpoints
    /// </summary>
    /// <param name="filters"></param>
    /// <returns></returns>
    [HttpGet("get", Name = "GetListEndpointsAsync")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [OpenApiOperation("Retorna una lista de todos los endpoints", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<EndpointsDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> GetListEndpointsAsync([FromQuery] EndpointsQueryFilter filters)
    {
        var endpoints = await _endpointsService.GetEndpoints(filters);

        var Metadata = new Metadata
        {
            TotalCount = endpoints.TotalCount,
            PageSize = endpoints.PageSize,
            CurrentPage = endpoints.CurrentPage,
            TotalPages = endpoints.TotalPages,
            HasNextPage = endpoints.HasNextPage,
            HasPreviousPage = endpoints.HasPreviousPage,
            NextPageUrl = _uriService.GetPaginationUri(filters, Url.RouteUrl(nameof(GetListEndpointsAsync))).ToString(),
            PreviousPageUrl = _uriService.GetPaginationUri(filters, Url.RouteUrl(nameof(GetListEndpointsAsync))).ToString()
        };

        var response = new ApiResponse<IEnumerable<EndpointsDto>>(endpoints)
        {
            Meta = Metadata
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(Metadata));

        return Ok(response);
    }

    /// <summary>
    /// Inserta un nuevo endpoint
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("create")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [OpenApiOperation("Inserta un nuevo endpoint", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<EndpointsDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> CreateEndpointAsync(CreateEndpointRequest request)
    {
        var endpointDto = await _endpointsService.InsertEndpoint(request);
        var response = new ApiResponse<EndpointsDto>(endpointDto);
        return Ok(response);
    }
    /// <summary>
    /// Actualiza un endpoint
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("update")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [OpenApiOperation("Actualiza un endpoint", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> UpdateEndpointAsync(UpdateEndpointRequest request)
    {
        var result = await _endpointsService.UpdateEndpoint(request);
        var response = new ApiResponse<bool>(result);
        return Ok(response);
    }

    /// <summary>
    /// Elimina un endpoint
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost("delete/{id}")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [OpenApiOperation("Elimina un endpoint", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> DeleteEndpointAsync(int id)
    {
        var result = await _endpointsService.DeleteEndpoint(id);
        var response = new ApiResponse<bool>(result);
        return Ok(response);
    }

    /// <summary>
    /// Asigna un permiso a un endpoint
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("assign-permiso")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [OpenApiOperation("Asigna un permiso a un endpoint", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> AssignEndpointToPermisoAsync(CreateOrUpdateEndpointPermisoRequest request)
    {
        var permisoEnpointDto = await _endpointsService.AddEndpointPermiso(request);
        var response = new ApiResponse<bool>(true);
        return Ok(response);
    }
}
