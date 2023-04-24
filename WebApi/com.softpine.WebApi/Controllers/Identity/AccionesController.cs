using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NSwag.Annotations;
using Matrices.Api.Responses;
using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.infrastructure.Shared.Middleware;
using com.softpine.muvany.infrastructure.Identity.Auth.Permissions;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.QueryFilters;
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.Requests;

namespace com.softpine.muvany.WebApi.Controllers;
/// <summary>
/// Procesos de los mantenimientos de Acciones 
/// </summary>
public class AccionesController : VersionNeutralApiController
{
    private readonly IUriService _uriService;
    private readonly IAccionesService accionesService;
    private readonly IMailService _mailService;

    /// <summary>
    /// Contructor 
    /// </summary>
    /// <param name="accionesService"></param>
    /// <param name="uriService"></param>
    public AccionesController(IAccionesService accionesService, IUriService uriService, IMailService mailService)
    {

        this.accionesService = accionesService;
        _uriService = uriService;
        _mailService = mailService;

    }


    /// <summary>
    /// Para obtener Acciones
    /// </summary>
    /// <param name="filters">Para filtrar</param>
    /// <returns>Lista de las Acciones</returns>
    [HttpGet("get")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [OpenApiOperation("Retorna una lista de todos las Acciones", "Lista de todos las Acciones si deseas pueden ser filtradas")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<AccionesDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> GetListAccionesAsync([FromQuery] AccionesQueryFilter filters)
    {
        var acciones = await accionesService.GetAllAsync(filters);

        var Metadata = new Metadata
        {
            TotalCount = acciones.TotalCount,
            PageSize = acciones.PageSize,
            CurrentPage = acciones.CurrentPage,
            TotalPages = acciones.TotalPages,
            HasNextPage = acciones.HasNextPage,
            HasPreviousPage = acciones.HasPreviousPage,
            NextPageUrl = _uriService.GetPaginationUri(new BasePostQueryFilter { PageSize = filters.PageSize, PageNumber = acciones.HasNextPage ? filters.PageNumber + 1 : filters.PageNumber }, "/api/acciones/get").ToString()
        };

        var response = new ApiResponse<IEnumerable<AccionesDto>>(acciones)
        {
            Meta = Metadata
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(Metadata));
        await _mailService.SendEmailSoftpine("softpineadmin@softpine.net", "Esto es probando el correo", "Probadera en Get Acciones");
        
        return Ok(response);
    }

    /// <summary>
    /// Inserta una nueva Acciones
    /// </summary>
    /// <param name="request">Objeto Acciones que va a ser agregado</param>
    /// <returns>true o false</returns>
    [HttpPost("create")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [OpenApiOperation("Inserta una nueva Acciones", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<AccionesDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> CreateAccionesAsync(CreateAccionesRequest request)
    {

        var accionesDto = await accionesService.CreateAsync(request);
        var response = new ApiResponse<AccionesDto>(accionesDto);
        return Ok(response);
    }
    /// <summary>
    /// Actualiza una Acciones
    /// </summary>
    /// <param name="request">Objeto Acciones que va a ser agregado</param>
    /// <returns>true o false</returns>
    [HttpPut("update")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [OpenApiOperation("Actualiza una Acciones", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> UpdateAccionesAsync(UpdateAccionesRequest request)
    {
        var result = await accionesService.UpdateAsync(request);
        var response = new ApiResponse<bool>(result);
        return Ok(response);
    }

    /// <summary>
    /// Elimina una Acciones
    /// </summary>
    /// <param name="id">El Id que de desea eliminar</param>
    /// <returns>true o false</returns>
    //[HttpDelete("delete/{id}")]
    ////[Authorization("Authorization")]
    //[AllowAnonymous]
    //[OpenApiOperation("Elimina una Acciones", "")]
    //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    //public async Task<IActionResult> DeleteAccionesAsync(int id)
    //{
    //    var result = await accionesService.DeleteAsync(id);
    //    var response = new ApiResponse<bool>(result);
    //    return Ok(response);
    //}



}

