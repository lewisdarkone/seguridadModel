using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.QueryFilters;
using com.softpine.muvany.models.Requests;

namespace com.softpine.muvany.core.Interfaces
{
    /// <summary>
    /// Interfaz para los procesos relacionados a el mantenimiento de Endpoints
    /// </summary>
    public interface IEndpointsService : IScopedService
    {
        /// <summary>
        /// Funcion que retorna una lista de Endpoints
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        Task<PagedList<EndpointsDto>> GetEndpoints(EndpointsQueryFilter filters);

        /// <summary>
        /// Funcion que retorna un Endpoint por su Sucursal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EndpointsDto> GetEndpoint(int id);

        /// <summary>
        /// Funcion para insertar un nuevo Endpoint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EndpointsDto> InsertEndpoint(CreateEndpointRequest request);

        /// <summary>
        /// Funcion para actualizar un Endpoint por su Sucursal
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> UpdateEndpoint(UpdateEndpointRequest item);

        /// <summary>
        /// Funcion para Desactivar un Endpoint por su Sucursal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteEndpoint(int id);

        /// <summary>
        /// Funcion para agregar permisos a un Endpoint por su Sucursal
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddEndpointPermiso(CreateOrUpdateEndpointPermisoRequest request);
    }
}
