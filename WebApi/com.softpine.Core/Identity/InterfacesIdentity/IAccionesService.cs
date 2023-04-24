using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.QueryFilters;
using com.softpine.muvany.models.Requests;

namespace com.softpine.muvany.core.Identity.InterfacesIdentity;
/// <summary>
/// Interfaz para el servicio del mantenimiento de Acciones
/// </summary>
public interface IAccionesService : ITransientService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="acciones"></param>
    /// <returns></returns>
    Task<AccionesDto> CreateAsync(CreateAccionesRequest acciones);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<PagedList<AccionesDto>> GetAllAsync(AccionesQueryFilter filters);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(UpdateAccionesRequest request);
}




