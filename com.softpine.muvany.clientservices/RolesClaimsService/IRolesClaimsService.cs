using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.ResponseModels.RoleClaims;

namespace com.softpine.muvany.clientservices;

public interface IRolesClaimsService
{
    /// <summary>
    /// Retorna los registros de los roles claims y acepta algunos parametros como query
    /// </summary>
    /// <param name="query">Sucursal=123, ClaimValue=abc, PageSize=123, PageNumber=123</param>
    /// <returns></returns>
    Task<GetRolClaimsListResponse?> GetRoleClaims(string query="");
    /// <summary>
    /// Retorna los permisos de un rol
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<GetRoleClaimsPermissionResponse?> GetRoleClaimsPermission(string id);

    Task<RoleClaimsResponse?> AssingRoleClaims(CreateRolePermissionsRequest assingRoleClaimRequest);
    /// <summary>
    /// Actualiza los permisos de un Rol
    /// </summary>
    /// <param name="assingRoleClaimRequest"></param>
    /// <returns></returns>
    Task<RoleClaimsResponse?> UpdateRoleClaims(CreateRolePermissionsRequest assingRoleClaimRequest);
    Task<RoleClaimsResponse?> DeleteRoleClaims(CreateRolePermissionsRequest assingRoleClaimRequest);
}
