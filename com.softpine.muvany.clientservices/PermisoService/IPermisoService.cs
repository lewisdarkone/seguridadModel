

using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.ResponseModels.PermisoResponse;

namespace com.softpine.muvany.clientservices;

public interface IPermisoService
{
    Task<GetPermisosResponse?> GetPermisos(string query="");
    Task<CreatePermisoResponse?> CreatePermiso(CreateOrUpdatePermisoRequest createPermisoRequest);
    Task<PermisoResponse?> UpdatePermiso(CreateOrUpdatePermisoRequest createPermisoRequest);
    Task<PermisoResponse?> DeletePermiso(string permisoId);
}
