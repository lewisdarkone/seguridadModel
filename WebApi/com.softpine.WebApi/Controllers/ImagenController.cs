
using com.softpine.muvany.models.Interfaces.InterfacesServices;

namespace com.softpine.muvany.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ImagenController : VersionNeutralApiController
    {
        private readonly IImagenService imagenService;
        private readonly IConfiguration configuration;

        /// <summary>
        /// 
        /// </summary>
        public ImagenController(IImagenService imagenService, IConfiguration configuration)
        {
            this.imagenService = imagenService;
            this.configuration = configuration;
        }

        /// <summary>
        /// Inserta una nueva Accesorios
        /// </summary>
        /// <param name="request">Objeto Accesorios que va a ser agregado</param>
        /// <returns></returns>
        //[HttpPost("optimizar")]
        //[Authorization("Authorization")]
        //[OpenApiOperation("Inserta una nueva Accesorios", "")]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<JsonResult>))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResult))]
        //public async Task<IActionResult> OptimizarImagenAsync(ImagenRequest request)
        //{
        //    var accesoriosDto = await imagenService.OptimizarImagenAsync(request, configuration.GetValue<int>("Imagen:Width"));
        //    var response = new ApiResponse<string>("["+string.Join(",", accesoriosDto.ByteArrayImg)+"]");
        //    return Ok(response);
        //}
    }
}
