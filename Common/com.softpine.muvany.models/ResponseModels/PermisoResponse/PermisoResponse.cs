

namespace com.softpine.muvany.models.ResponseModels.PermisoResponse
{
    public class PermisoResponse : BaseResponse
    {
        public bool Done { get; set; }
        public ExceptionResponse? Exception { get; set; }
    }
}
