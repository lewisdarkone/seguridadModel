



using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;

namespace com.softpine.muvany.models.ResponseModels.UserResponses;

public class GetUserResponse : BaseResponse
{

    public UserData? Data { get; set; }
    public ExceptionResponse? Exception { get; set; }

}

public class UserData{
    public UserDetailsDto? Data { get; set; }
    public Metadata? Meta { get; set; }
}
