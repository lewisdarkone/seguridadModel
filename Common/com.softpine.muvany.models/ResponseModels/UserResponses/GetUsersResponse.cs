using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;

namespace com.softpine.muvany.models.ResponseModels.UserResponses;

public class GetUsersResponse : BaseResponse
{
    public UserListResponse? Data { get; set; }
    public ExceptionResponse? Exception { get; set; }

}

public class UserListResponse
{
    public ICollection<UserDetailsDto>? Data { get; set; }
    public Metadata? Meta { get; set; }
}
