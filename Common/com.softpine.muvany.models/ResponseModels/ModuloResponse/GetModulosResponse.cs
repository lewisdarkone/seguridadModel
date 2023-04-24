using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;

namespace com.softpine.muvany.models.ResponseModels.ModuloResponse;

public class GetModulosResponse : BaseResponse
{
    public GetAllModuloData? Data { get; set; }
    
    public ExceptionResponse? Exception { get; set; }
}

public class GetAllModuloData {
    public ICollection<ModulosDto>? Data { get; set; }
    public Metadata? Meta { get; set; }
}
