

namespace com.softpine.muvany.models.ResponseModels.TokenResponse;

public class GetTokenResponse : BaseResponse
{
    public GetTokenData? Data { get; set; }
    public ExceptionResponse? Exception { get; set; }
}
