using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NSwag.Annotations;
using Matrices.Api.Responses;
using com.softpine.muvany.core.Contracts;
using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.infrastructure.Identity.Auth.Permissions;
using com.softpine.muvany.infrastructure.Shared.Middleware;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.QueryFilters;
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.Requests;

namespace com.softpine.muvany.WebApi.Controllers;
/// <summary>
/// Procesos de los mantenimientos de Recursos 
/// </summary>
public class RecursosController : VersionNeutralApiController
{
    private readonly IUriService _uriService;
    private readonly IRecursosService recursosService;

    /// <summary>
    /// Contructor 
    /// </summary>
    /// <param name="recursosService"></param>
    /// <param name="uriService"></param>
    public RecursosController(IRecursosService recursosService, IUriService uriService)
    {

        this.recursosService = recursosService;
        _uriService = uriService;

    }


    /// <summary>
    /// Para obtener el Menu
    /// </summary>
    /// <returns>Lista del Menu</returns>
    [HttpGet("get/menu", Name = "GetListMenuAsync")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [OpenApiOperation("Retorna una lista del Menu", "Lista de todos los Recursos del Menu para el Usuario como corresponde")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<Menu>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> GetListMenuAsync()
    {
        var recursos = await recursosService.GetRecursosWithIdUser();


        var response = new ApiResponse<IEnumerable<ModulosDto>>(recursos);


        return Ok(response);
    }

    /// <summary>
    /// Para obtener Recursos
    /// </summary>
    /// <param name="filters">Para filtrar</param>
    /// <returns>Lista de las Recursos</returns>
    [HttpGet("get", Name = "GetListRecursosAsync")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [OpenApiOperation("Retorna una lista de todos las Recursos", "Lista de todos las Recursos si deseas pueden ser filtradas")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<RecursosDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> GetListRecursosAsync([FromQuery] RecursosQueryFilter filters)
    {
        var recursos = await recursosService.GetAllAsync(filters);

        var Metadata = new Metadata
        {
            TotalCount = recursos.TotalCount,
            PageSize = recursos.PageSize,
            CurrentPage = recursos.CurrentPage,
            TotalPages = recursos.TotalPages,
            HasNextPage = recursos.HasNextPage,
            HasPreviousPage = recursos.HasPreviousPage,
            NextPageUrl = _uriService.GetPaginationUri(new BasePostQueryFilter { PageSize = filters.PageSize, PageNumber = (recursos.HasNextPage) ? (filters.PageNumber + 1) : filters.PageNumber }, "/api/recursos/get").ToString()
        };

        var response = new ApiResponse<IEnumerable<RecursosDto>>(recursos)
        {
            Meta = Metadata
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(Metadata));

        return Ok(response);
    }

    /// <summary>
    /// Inserta una nueva Recursos
    /// </summary>
    /// <param name="request">Objeto Recursos que va a ser agregado</param>
    /// <returns>true o false</returns>
    [HttpPost("create")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [OpenApiOperation("Inserta una nueva Recursos", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<RecursosDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> CreateRecursosAsync(CreateRecursosRequest request)
    {

        var recursosDto = await recursosService.CreateAsync(request);
        var response = new ApiResponse<RecursosDto>(recursosDto);
        return Ok(response);
    }
    /// <summary>
    /// Actualiza una Recursos
    /// </summary>
    /// <param name="request">Objeto Recursos que va a ser agregado</param>
    /// <returns>true o false</returns>
    [HttpPut("update")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [OpenApiOperation("Actualiza una Recursos", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> UpdateRecursosAsync(UpdateRecursosRequest request)
    {
        var result = await recursosService.UpdateAsync(request);
        var response = new ApiResponse<bool>(result);
        return Ok(response);
    }

    /// <summary>
    /// Elimina una Recursos
    /// </summary>
    /// <param name="id">El Id que de desea eliminar</param>
    /// <returns>true o false</returns>
    //[HttpDelete("delete/{id}")]
    //[Authorization("Authorization")]
    //[AllowAnonymous]
    //[OpenApiOperation("Elimina una Recursos", "")]
    //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    //public async Task<IActionResult> DeleteRecursosAsync(int id)
    //{
    //    var result = await recursosService.DeleteAsync(id);
    //    var response = new ApiResponse<bool>(result);
    //    return Ok(response);
    //}



}

