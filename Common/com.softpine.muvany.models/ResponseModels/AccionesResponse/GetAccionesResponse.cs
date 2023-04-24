
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;

namespace com.softpine.muvany.models.ResponseModels.AccionesResponse
{
    public class GetAccionesResponse: BaseResponse
    {
        public GetAccionesData? Data { get; set; }
        public ExceptionResponse? Exception { get; set; }


    }
    public class GetAccionesData
    {
        public ICollection<AccionesDto>? Data { get; set; }
        public Metadata? Meta { get; set; }
    }
}
