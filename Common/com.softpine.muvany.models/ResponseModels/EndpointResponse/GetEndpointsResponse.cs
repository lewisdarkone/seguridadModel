
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;

namespace com.softpine.muvany.models.ResponseModels.EndpointResponse
{
    public class GetEndpointsResponse : BaseResponse
    {
        public GetEndPointData? Data { get; set; }
        public ExceptionResponse? Exception { get; set; }
    }

    public class GetEndPointData
    {
        public ICollection<EndpointsDto>? Data { get; set; }
        public Metadata? Meta { get; set; }
    }
}
