using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.models.Entities.EntitiesIdentity;
using com.softpine.muvany.models.Interfaces;

namespace com.softpine.muvany.core.Identity.InterfacesIdentity;

/// <summary>
/// Interfaz para los procesos del Mantenimiento de Endpoint
/// </summary>
public interface IEndpointsRepository : IScopedService, IRepositoryIdentity<Endpoints>
{
    /// <summary>
    /// Recurso para Agregar un a nuevo Endpoint
    /// </summary>
    /// <param name="endpoint">Parametro de tipo Endpoint</param>
    /// <returns></returns>
    Task<Endpoints> AddEndpoint(Endpoints endpoint);
    /// <summary>
    /// Recurso para obtener un Endpoint activo
    /// </summary>
    /// <param name="controller">Nombre del controlador</param>
    /// <param name="action">Acción del controlador</param>
    /// <param name="httpVerb">Verbo del controlador</param>
    /// <returns></returns>
    Task<Endpoints> GetActiveEndpoint(string controller, string action, string httpVerb);
    /// <summary>
    /// Recurso para obtener todos los Enpoint
    /// </summary>
    /// <returns></returns>
    Task<List<Endpoints>> GetAllEndpoints();
    /// <summary>
    /// Recurso para obtener un Endpoint por su Sucursal
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Endpoints> GetEndpointById(int id);
    /// <summary>
    /// Recurso para obtener los permisos relacionados a un Endpoint
    /// </summary>
    /// <param name="controller">Nombre del controlador</param>
    /// <param name="action">Acción del controlador</param>
    /// <returns></returns>
    Task<Endpoints> GetActiveEndpointPermisos(string controller, string action);

}
