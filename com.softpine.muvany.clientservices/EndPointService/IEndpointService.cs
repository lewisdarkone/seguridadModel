

using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.ResponseModels.EndpointResponse;

namespace com.softpine.muvany.clientservices;

public interface IEndpointService
{

    /// <summary>
    /// Obtiene una lista de Endpoints que puede ser filtrado por parametros en el query
    /// </summary>
    /// <param name="query">Sucursal=123, Nombre=abc,Controlador=abc,Metodo=abc, HttpVerbo=abc, IsActive=true/false, PageSize=123, PageNumber=123</param>    
    /// <returns>PermisoResponse?</returns>
    Task<GetEndpointsResponse?> GetEndpoints(string query="");

    /// <summary>
    /// Crear un nuevo endpoint
    /// </summary>
    /// <param name="endpointRequest">Objeto que contiene los atributos necesarios para crear un nuevo endpoint</param>    
    /// <returns>PermisoResponse?</returns>
    Task<CreateEndpointsResponse?> CreateEndpoints(CreateEndpointRequest endpointRequest);

    /// <summary>
    /// Actualiza un Endpoint
    /// </summary>
    /// <param name="endpointRequest">Objeto con las propiedades necesarias para actualizar un endpoint</param>    
    /// <returns>PermisoResponse?</returns>
    Task<UpdateEndpointsResponse?> UpdateEndpoints(UpdateEndpointRequest endpointRequest);

    /// <summary>
    /// Elimina un endpoint
    /// </summary>
    /// <param name="endpointId">Sucursal del endpoint a eliminar</param>
    /// <returns>PermisoResponse?</returns>
    Task<UpdateEndpointsResponse?> DeleteEndpoints(string endpointId);

    /// <summary>
    /// Asigna un permiso a un endpoint
    /// </summary>
    /// <param name="assignEndpointRequest">Objeto que contiene los atributos para asignar un permiso a un endpoint, ej:permisoId,endpointId</param>
    /// <returns>AssignEndpointResponse?</returns>
    Task<UpdateEndpointsResponse?> AssignEndpointPermiso(CreateOrUpdateEndpointPermisoRequest assignEndpointRequest);
}
