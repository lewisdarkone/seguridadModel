
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;

namespace com.softpine.muvany.models.ResponseModels.RoleResponse;

public class GetRolesListResponse : BaseResponse
{
    public GetRoleListData? Data { get; set; }
    public ExceptionResponse? Exception { get; set; }
}

public class GetRoleListData {
    public ICollection<RoleDto>? Data { get; set; }
    public Metadata? Meta { get; set; }
}
