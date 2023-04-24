

namespace com.softpine.muvany.models.ResponseModels.UserResponses
{
    public class ResetPasswordResponse : BaseResponse
    {
        public BasicResponse? Data { get; set; }
        public ExceptionResponse? Exception { get; set; }
    }
}
