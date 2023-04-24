
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;

namespace com.softpine.muvany.models.ResponseModels.UserResponses;

public class GetRolesResponse : BaseResponse
{
    public UserRolesData? Data { get; set; }
    public ExceptionResponse? Exception { get; set; }
}

public class UserRolesData {
    public ICollection<UserRoleDto>? Data { get; set; }
    public Metadata? Meta { get; set; }
}
