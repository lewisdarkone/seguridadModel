
using com.softpine.muvany.models.Request.RequestsIdentity;
using com.softpine.muvany.models.ResponseModels;
using com.softpine.muvany.models.ResponseModels.ModuloResponse;

namespace com.softpine.muvany.clientservices;

public interface IModuloService
{
    Task<GetModulosResponse> GetAll(string? query="");
    Task<ModuloResponse?> Add(CreateModulosRequest addModuloRequest);
    Task<UpdateResponse?> Update(UpdateModulosRequest modulo);
    Task<UpdateResponse?> Delete(string moduloId);

}
