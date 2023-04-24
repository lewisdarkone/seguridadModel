

using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;


namespace com.softpine.muvany.models.ResponseModels.RecursoResponse;

public class CreateRecursoResponse : BaseResponse
{
    public CreateRecursoData? Data { get; set; }
    public ExceptionResponse? Exception { get; set; }
}
public class CreateRecursoData
{
    public RecursosDto? Data { get; set; }
    public Metadata? Meta { get; set; }
}
