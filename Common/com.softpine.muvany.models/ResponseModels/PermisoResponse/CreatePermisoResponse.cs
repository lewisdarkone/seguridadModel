
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;

namespace com.softpine.muvany.models.ResponseModels.PermisoResponse
{
    public class CreatePermisoResponse : BaseResponse
    {
        public CreateDataResponse? Data { get; set; }
        public ExceptionResponse? Exception { get; set; }
    }
    public class CreateDataResponse
    {
        public PermisosDto? Data { get; set; }
        public Metadata? Meta { get; set; }
    }
}
