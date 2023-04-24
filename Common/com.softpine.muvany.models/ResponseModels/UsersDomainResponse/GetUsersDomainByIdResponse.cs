using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.Request.RequestsIdentity;

namespace com.softpine.muvany.models.ResponseModels.UsersDomainResponse;

public class GetUsersDomainByIdResponse : BaseResponse
{
    public GetUsersDomainByIdData? Data { get; set; }
    public ExceptionResponse? Exception { get; set; }
}

public class GetUsersDomainByIdData
{
    public UserDomainRequest? Data { get; set; }
    public Metadata? Meta { get; set; }
}
