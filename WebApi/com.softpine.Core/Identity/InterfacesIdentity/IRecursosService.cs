using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.QueryFilters;
using com.softpine.muvany.models.Requests;

namespace com.softpine.muvany.core.Identity.InterfacesIdentity;
/// <summary>
/// Interfaz para el servicio del mantenimiento de Recursos
/// </summary>
public interface IRecursosService : ITransientService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="recursos"></param>
    /// <returns></returns>
    Task<RecursosDto> CreateAsync(CreateRecursosRequest recursos);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filters"></param>
    /// <returns></returns>
    Task<PagedList<RecursosDto>> GetAllAsync(RecursosQueryFilter filters);
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ModulosDto>> GetRecursosWithIdUser();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(UpdateRecursosRequest request);
}




