using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.models.Entities.EntitiesIdentity;

namespace com.softpine.muvany.core.Identity.InterfacesIdentity;

/// <summary>
/// 
/// </summary>
public interface IRecursosRepository : IRepositoryIdentity<Recursos>
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Recursos>> GetAllRecursos();
}
