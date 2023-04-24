using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NSwag.Annotations;
using Matrices.Api.Responses;
using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.infrastructure.Identity.Auth.Permissions;
using com.softpine.muvany.infrastructure.Shared.Middleware;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.QueryFilters;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.Request.RequestsIdentity;

namespace com.softpine.muvany.WebApi.Controllers;
/// <summary>
/// Procesos de los mantenimientos de Modulos 
/// </summary>
public class ModulosController : VersionNeutralApiController
{
private readonly IUriService _uriService; 
		private readonly IModulosService modulosService;
		
		/// <summary>
		/// Contructor 
		/// </summary>
		/// <param name="modulosService"></param>
		/// <param name="uriService"></param>
		public ModulosController(IModulosService modulosService, IUriService uriService)
		{

			this.modulosService = modulosService;
			_uriService = uriService;

		}
		
		
    /// <summary>
    /// Para obtener Modulos
    /// </summary>
    /// <param name="filters">Para filtrar</param>
    /// <returns>Lista de las Modulos</returns>
    [HttpGet("get")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [OpenApiOperation("Retorna una lista de todos las Modulos", "Lista de todos las Modulos si deseas pueden ser filtradas")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<ModulosDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> GetListModulosAsync([FromQuery] ModulosQueryFilter filters)
    {
        var modulos = await modulosService.GetAllAsync(filters);

        var Metadata = new Metadata
        {
            TotalCount = modulos.TotalCount,
            PageSize = modulos.PageSize,
            CurrentPage = modulos.CurrentPage,
            TotalPages = modulos.TotalPages,
            HasNextPage = modulos.HasNextPage,
            HasPreviousPage = modulos.HasPreviousPage,
            NextPageUrl = _uriService.GetPaginationUri(new BasePostQueryFilter { PageSize=filters.PageSize, PageNumber = (modulos.HasNextPage)?(filters.PageNumber+1):filters.PageNumber }, "/api/modulos/get").ToString()
        };

        var response = new ApiResponse<IEnumerable<ModulosDto>>(modulos)
        {
            Meta = Metadata
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(Metadata));

        return Ok(response);
    }

    /// <summary>
    /// Inserta una nueva Modulos
    /// </summary>/// <remarks>
    /// <param name="request">Objeto Modulos que va a ser agregado</param>
    /// <returns>true o false</returns>
    [HttpPost("create")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [OpenApiOperation("Inserta una nueva Modulos", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<ModulosDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> CreateModulosAsync(CreateModulosRequest request)
    {
        var modulosDto = await modulosService.CreateAsync(request);
        var response = new ApiResponse<ModulosDto>(modulosDto);
        return Ok(response);
    }
    /// <summary>
    /// Actualiza una Modulos
    /// </summary>
    /// <param name="request">Objeto Modulos que va a ser agregado</param>
    /// <returns>true o false</returns>
    [HttpPut("update")]
    [Authorization("Authorization")]
    //[AllowAnonymous]
    [OpenApiOperation("Actualiza una Modulos", "")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> UpdateModulosAsync(UpdateModulosRequest request)
    {
        var result = await modulosService.UpdateAsync(request);
        var response = new ApiResponse<bool>(result);
        return Ok(response);
    }

    /// <summary>
    /// Elimina una Modulos
    /// </summary>
    /// <param name="id">El Sucursal que de desea eliminar</param>
    /// <returns>true o false</returns>
    //[HttpDelete("delete/{id}")]
    //[Authorization("Authorization")]
    ////[AllowAnonymous]
    //[OpenApiOperation("Elimina una Modulos", "")]
    //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    //public async Task<IActionResult> DeleteModulosAsync(int id)
    //{
    //    var result = await modulosService.DeleteAsync(id);
    //    var response = new ApiResponse<bool>(result);
    //    return Ok(response);
    //}
		
		

}

