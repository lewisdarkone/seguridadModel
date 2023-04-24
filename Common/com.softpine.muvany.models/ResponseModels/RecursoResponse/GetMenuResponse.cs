

using com.softpine.muvany.core.Contracts;
using com.softpine.muvany.models.CustomEntities;

namespace com.softpine.muvany.models.ResponseModels.RecursoResponse;

public class GetMenuResponse: BaseResponse
{
    public GetMenuData? Data { get; set; }
    public ExceptionResponse? Exception { get; set; }

}

public class GetMenuData
{
    public ICollection<Menu>? Data { get; set; }
    public Metadata? Meta { get; set; }
}
