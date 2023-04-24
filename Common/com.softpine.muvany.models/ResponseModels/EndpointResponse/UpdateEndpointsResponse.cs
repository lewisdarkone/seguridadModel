

using com.softpine.muvany.models.CustomEntities;

namespace com.softpine.muvany.models.ResponseModels.EndpointResponse
{
    public class UpdateEndpointsResponse : BaseResponse
    {
        public UpdateEndPointData? Data { get; set; }
        public ExceptionResponse? Exception { get; set; }
    }

    public class UpdateEndPointData
    {
        public bool Data { get; set; }
        public Metadata? Meta { get; set; }
    }
}
