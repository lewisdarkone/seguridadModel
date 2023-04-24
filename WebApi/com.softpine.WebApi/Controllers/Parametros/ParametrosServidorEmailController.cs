using System.Net;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.Interfaces.InterfacesServices;
using Microsoft.AspNetCore.Mvc;
namespace com.softpine.muvany.WebApi.Controllers;
/// <summary>
/// Procesos de los mantenimientos de ParametrosServidorEmail 
/// </summary>
[Route("api/parametros-servidor-email")]
public class ParametrosServidorEmailController : VersionNeutralApiController
{
private readonly IUriService _uriService; 
		private readonly IParametrosServidorEmailService parametrosServidorEmailService;
		
		/// <summary>
		/// Contructor 
		/// </summary>
		/// <param name="parametrosServidorEmailService"></param>
		/// <param name="uriService"></param>
		public ParametrosServidorEmailController(IParametrosServidorEmailService parametrosServidorEmailService, IUriService uriService)
		{

			this.parametrosServidorEmailService = parametrosServidorEmailService;
			_uriService = uriService;

		}
		
		
    /// <summary>
    /// Para obtener ParametrosServidorEmail
    /// </summary>
    /// <param name="filters">Para filtrar</param>
    /// <returns>Lista de las ParametrosServidorEmail</returns>
    //[HttpGet("get")]
    //[Authorization("Authorization")]
    //[OpenApiOperation("Retorna una lista de todos las ParametrosServidorEmail", "")]
    //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<ParametrosServidorEmailDto>>))]
    //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    //public async Task<IActionResult> GetListParametrosServidorEmailAsync([FromQuery] ParametrosServidorEmailQueryFilter filters)
    //{
    //    var parametrosServidorEmail = await parametrosServidorEmailService.GetAllAsync(filters);

    //    var Metadata = new Metadata
    //    {
    //        TotalCount = parametrosServidorEmail.TotalCount,
    //        PageSize = parametrosServidorEmail.PageSize,
    //        CurrentPage = parametrosServidorEmail.CurrentPage,
    //        TotalPages = parametrosServidorEmail.TotalPages,
    //        HasNextPage = parametrosServidorEmail.HasNextPage,
    //        HasPreviousPage = parametrosServidorEmail.HasPreviousPage,
    //        NextPageUrl = _uriService.GetPaginationUri(new BasePostQueryFilter { PageSize=filters.PageSize, PageNumber = (parametrosServidorEmail.HasNextPage)?(filters.PageNumber+1):filters.PageNumber }, "/api/parametrosServidorEmail/get").ToString()
    //    };

    //    var response = new ApiResponse<IEnumerable<ParametrosServidorEmailDto>>(parametrosServidorEmail)
    //    {
    //        Meta = Metadata
    //    };

    //    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(Metadata));

    //    return Ok(response);
    //}

    /// <summary>
    /// Inserta una nueva ParametrosServidorEmail
    /// </summary>/// <remarks>
    /// <param name="request">Objeto ParametrosServidorEmail que va a ser agregado</param>
    /// <returns>true o false</returns>
    //[HttpPost("create")]
    //[Authorization("Authorization")]
    //[OpenApiOperation("Inserta una nueva ParametrosServidorEmail", "")]
    //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<ParametrosServidorEmailDto>))]
    //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    //public async Task<IActionResult> CreateParametrosServidorEmailAsync(CreateParametrosServidorEmailRequest request)
    //{
        
    //    var parametrosServidorEmailDto = await parametrosServidorEmailService.CreateAsync(request);
    //    var response = new ApiResponse<ParametrosServidorEmailDto>(parametrosServidorEmailDto);
    //    return Ok(response);
    //}
    /// <summary>
    /// Actualiza una ParametrosServidorEmail
    /// </summary>
    /// <param name="request">Objeto ParametrosServidorEmail que va a ser agregado</param>
    /// <returns>true o false</returns>
    //[HttpPut("update")]
    //[Authorization("Authorization")]
    //[OpenApiOperation("Actualiza una ParametrosServidorEmail", "")]
    //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    //public async Task<IActionResult> UpdateParametrosServidorEmailAsync(UpdateParametrosServidorEmailRequest request)
    //{
    //    var result = await parametrosServidorEmailService.UpdateAsync(request);
    //    var response = new ApiResponse<bool>(result);
    //    return Ok(response);
    //}

    /// <summary>
    /// Elimina una ParametrosServidorEmail
    /// </summary>
    /// <param name="id">El Sucursal que de desea eliminar</param>
    /// <returns>true o false</returns>
    //[HttpDelete("delete/{id}")]
    //[Authorization("Authorization")]
    //[OpenApiOperation("Elimina una ParametrosServidorEmail", "")]
    //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
    //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
    //public async Task<IActionResult> DeleteParametrosServidorEmailAsync(int id)
    //{
    //    var result = await parametrosServidorEmailService.DeleteAsync(id);
    //    var response = new ApiResponse<bool>(result);
    //    return Ok(response);
    //}
		
		

}

