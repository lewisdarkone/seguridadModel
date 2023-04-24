

using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.ResponseModels.RoleResponse;

namespace com.softpine.muvany.clientservices;

public interface IRoleService
{
    /// <summary>
    /// Retorna una lista de roles y acepta filtro por parametros en el query
    /// </summary>
    /// <param name="token"></param>
    /// <param name="query">Sucursal=abc, DescripTipoDoc=abc, PageSize=123, PageNumber=123</param>
    /// <returns></returns>
    Task<GetRolesListResponse?> GetRoles(string query ="");
    Task<GetRoleByIdResponse?> GetRoleById(string roleId);
    Task<CreateRoleResponse?> CreateRole(CreateRoleRequest createRoleRequest);
    Task<CreateRoleResponse?> UpdateRole(UpdateRoleRequest updateRoleRequest);
    Task<DeleteRoleResponse?> DeleteRole(string roleId);
}
