using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.models.Entities.EntitiesIdentity;
using com.softpine.muvany.models.Interfaces;

namespace com.softpine.muvany.core.Identity.InterfacesIdentity;

/// <summary>
/// 
/// </summary>
public interface IPermisosRepository : IRepositoryIdentity<Permisos>, ITransientService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="permiso"></param>
    /// <returns></returns>
    Task<Permisos> AddPermiso(Permisos permiso);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="wildcard"></param>
    /// <returns></returns>
    Task<List<Permisos>> GetPermisos(string wildcard);
}
