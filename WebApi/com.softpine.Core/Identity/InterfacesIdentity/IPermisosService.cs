using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.QueryFilters;
using com.softpine.muvany.models.Request.RequestsIdentity;
using com.softpine.muvany.models.Requests;

namespace com.softpine.muvany.core.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPermisosService: IScopedService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        Task <PagedList<PermisosDto>>GetPermisos(PermisosQueryFilter filters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PermisosDto> GetPermiso(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PermisosDto> InsertPermiso(CreateOrUpdatePermisoRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> UpdatePermiso(CreateOrUpdatePermisoRequest item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeletePermiso(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="httpVerb"></param>
        /// <returns></returns>
        Task<PermisosDto> GetPermisoEndpoint(string controller, string action, string httpVerb);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="resource"></param>
        /// <returns></returns>
        Task<PermissionsRequest> GetPermisosClaims(string action, string resource);
    }
}
