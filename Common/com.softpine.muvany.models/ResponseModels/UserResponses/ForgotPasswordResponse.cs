


using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.Requests;

namespace com.softpine.muvany.models.ResponseModels.UserResponses;

public class ForgotPasswordResponse : BaseResponse
{
    public ForgotPasswordData? Data { get; set; }
    public ExceptionResponse? Exception { get; set; }
}

public class ForgotPasswordData {
    public ResetPasswordRequest? Data { get; set; }
    public Metadata? Meta { get; set; }
}
