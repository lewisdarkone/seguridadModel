

using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;

namespace com.softpine.muvany.models.ResponseModels.RoleResponse
{
    public class GetRoleByIdResponse : BaseResponse
    {
        public GetRoleByIdData? Data { get; set; }
        public ExceptionResponse? Exception { get; set; }
    }

    public class GetRoleByIdData {
        public RoleDto? Data { get; set; }
        public Metadata? Meta { get; set; }
    }

    
}
