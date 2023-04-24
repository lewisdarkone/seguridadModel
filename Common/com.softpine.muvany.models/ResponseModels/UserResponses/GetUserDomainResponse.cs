

using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.Request.RequestsIdentity;

namespace com.softpine.muvany.models.ResponseModels.UserResponses;

public class GetUserDomainResponse : BaseResponse
{
    public GetUserDomainData? Data { get; set; }
    public ExceptionResponse? Exception { get; set; }
}

public class GetUserDomainData
{
    public UserDomainRequest? Data { get; set; }
    public Metadata? Meta { get; set; }
}
