
namespace com.softpine.muvany.models.ResponseModels.PersonalResponse
{
    public class UpdateProfileResponse : BaseResponse
    {
        public BasicResponse? Data { get; set; }
        public ExceptionResponse? Exception { get; set; }
    }
}
