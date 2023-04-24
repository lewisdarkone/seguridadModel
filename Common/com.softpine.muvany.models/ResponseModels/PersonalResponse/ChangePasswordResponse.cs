
namespace com.softpine.muvany.models.ResponseModels.PersonalResponse
{
    public class ChangePasswordResponse : BaseResponse
    {
        public BasicResponse? Data { get; set; }
        public ExceptionResponse? Exception { get; set; }
    }
}
