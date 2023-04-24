
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;

namespace com.softpine.muvany.models.ResponseModels.PermisoResponse
{
    public class GetPermisosResponse : BaseResponse
    {
        public GetPermisoData? Data { get; set; }
        public ExceptionResponse? Exception { get; set; }
    }

    public class GetPermisoData
    {
        public ICollection<PermisosDto>? Data { get; set; }
        public Metadata? Meta { get; set; }
    }
}
