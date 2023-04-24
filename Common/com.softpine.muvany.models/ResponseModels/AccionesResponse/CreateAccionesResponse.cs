

using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;

namespace com.softpine.muvany.models.ResponseModels.AccionesResponse
{
    public class CreateAccionesResponse : BaseResponse
    {
        public CreateAccionesData? Data { get; set; }
        public ExceptionResponse? Exception { get; set; }
    }

    public class CreateAccionesData
    {
        public AccionesDto? Data { get; set; }
        public Metadata? Meta { get; set; }
    }

}
