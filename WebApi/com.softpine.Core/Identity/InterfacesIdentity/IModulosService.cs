using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.QueryFilters;
using com.softpine.muvany.models.Request.RequestsIdentity;

namespace com.softpine.muvany.core.Identity.InterfacesIdentity;
/// <summary>
/// Interfaz para el servicio del mantenimiento de Modulos
/// </summary>
public interface IModulosService : ITransientService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="modulos"></param>
    /// <returns></returns>
    Task<ModulosDto> CreateAsync(CreateModulosRequest modulos);
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
    Task<PagedList<ModulosDto>> GetAllAsync(ModulosQueryFilter filters);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(UpdateModulosRequest request);
}




