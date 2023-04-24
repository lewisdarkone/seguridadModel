using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.QueryFilters;
using com.softpine.muvany.models.Requests;

namespace com.softpine.muvany.core.Identity.InterfacesIdentity;

/// <summary>
/// 
/// </summary>
public interface IRoleService : ITransientService
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<PagedList<RoleDto>> GetListAsync(RolesQueryFilter filters);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<int> GetCountAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="roleName"></param>
    /// <param name="excludeId"></param>
    /// <returns></returns>
    Task<bool> ExistsAsync(string roleName, string? excludeId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<RoleDto> GetByIdAsync(string id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<bool> CreateAsync(CreateRoleRequest request);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(UpdateRoleRequest request);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(string id);
}
