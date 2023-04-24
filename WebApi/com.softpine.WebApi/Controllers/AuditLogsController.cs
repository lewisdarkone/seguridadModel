using System.Net;
using MediatR;
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.models.Interfaces;

namespace com.softpine.muvany.WebApi.Controllers
{
    /// <summary>
    /// Controlador con las funcionalidades de los logs de auditorías de cambios 
    /// </summary>
    public class AuditLogsController : VersionNeutralApiController
    {
        private readonly IUriService _uriService;
        private ISender _mediator = null!;

        /// <summary>
        /// 
        /// </summary>
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

        /// <summary>
        /// Controlador para la inyeccion de las dependencias
        /// </summary>
        /// <param name="uriService"></param>
        public AuditLogsController(IUriService uriService)
        {
            _uriService = uriService;
        }

        /// <summary>
        /// Retorna una lista de todas las auditorías de cambios 
        /// </summary>
        /// <param name="request"></param>
        //[HttpGet("get")]
        //[Authorization("Authorization")]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<AuditLogsDto>>))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
        //[OpenApiOperation("Retorna una lista de todas las auditorías de cambios", "")]
        //public async Task<IActionResult> GetListAuditLogsAsync([FromQuery] GetMyAuditLogsRequest request)
        //{
        //    var _auditLogs = await Mediator.Send(request);

        //    var Metadata = new Metadata
        //    {
        //        TotalCount = _auditLogs.TotalCount,
        //        PageSize = _auditLogs.PageSize,
        //        CurrentPage = _auditLogs.CurrentPage,
        //        TotalPages = _auditLogs.TotalPages,
        //        HasNextPage = _auditLogs.HasNextPage,
        //        HasPreviousPage = _auditLogs.HasPreviousPage,
        //        NextPageUrl = _uriService.GetPaginationUri(request, Url.RouteUrl(nameof(GetListAuditLogsAsync))).ToString()
        //    };

        //    var response = new ApiResponse<IEnumerable<AuditLogsDto>>(_auditLogs)
        //    {
        //        Meta = Metadata
        //    };

        //    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(Metadata));

        //    return Ok(response);

        //}
    }
}
