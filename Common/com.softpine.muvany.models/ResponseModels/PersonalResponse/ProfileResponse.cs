
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;

namespace com.softpine.muvany.models.ResponseModels.PersonalResponse
{
    public class ProfileResponse : BaseResponse
    {
        public ProfileData? Data { get; set; }
        public ExceptionResponse? Exception { get; set; }
    }

    public class ProfileData
    {
        public UserDetailsDto? Data { get; set; }
        public Metadata? Meta { get; set; }   
    } 
}
