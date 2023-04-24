
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;

namespace com.softpine.muvany.models.ResponseModels.EndpointResponse
{
    public class CreateEndpointsResponse : BaseResponse
    {
        public CreateEndPointData? Data { get; set; }
        public ExceptionResponse? Exception { get; set; }
    }

    public class CreateEndPointData
    {
        public EndpointsDto? Data { get; set; }
        public Metadata? Meta { get; set; }
    }
}
