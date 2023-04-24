using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.QueryFilters;
using com.softpine.muvany.models.Requests;

namespace com.softpine.muvany.infrastructure.Identity.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRolesClaimService : ITransientService
    {
        //Task<List<RolesClaimDto>> GetListAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<RolesClaimDto> GetRolByIdWithPermissionsAsync(string roleId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> UpdatePermissionsRolAsync(UpdateRolePermissionsRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DeletePermissionsRolAsync(DeleteRolePermissionsRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddPermissionsRolAsync(CreateRolePermissionsRequest request);

        /// <summary>
        /// Funcion que retorna todos los RolesClaims Registrados 
        /// </summary>
        Task<PagedList<ApplicationRoleClaimDto>> GetRolesClaimAsync(ApplicationRoleClaimQueryFilter filters);
    }
}
