using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;

namespace com.softpine.muvany.models.ResponseModels;

public class GetProfileResponse : BaseResponse
{
    public GetProfileData? Data { get; set; }
    public ExceptionResponse? Exception { get; set; }
}

public class GetProfileData
{
    public UserDetailsDto? Data { get; set; }
    public Metadata? Meta { get; set; }
}
