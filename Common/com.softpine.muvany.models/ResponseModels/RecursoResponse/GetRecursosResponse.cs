


using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;

namespace com.softpine.muvany.models.ResponseModels.RecursoResponse;

public class GetRecursosResponse : BaseResponse
{
    public GetRecursosData? Data { get; set; }
    public ExceptionResponse? Exception { get; set; }

}
public class GetRecursosData
{
    public ICollection<RecursosDto>? Data { get; set; }
    public Metadata? Meta { get; set; }
}
