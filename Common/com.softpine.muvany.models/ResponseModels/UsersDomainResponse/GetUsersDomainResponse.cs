using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.Request.RequestsIdentity;

namespace com.softpine.muvany.models.ResponseModels.UsersDomainResponse;

public class GetUsersDomainResponse : BaseResponse
{
    public GetUsersDomainData? Data { get; set; }
    public ExceptionResponse? Exception { get; set; }
}

public class GetUsersDomainData
{
    public ICollection<UserDomainRequest>? Data { get; set; }
    public Metadata? Meta { get; set; }
}
