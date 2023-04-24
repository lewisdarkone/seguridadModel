using com.softpine.muvany.models.Request.RequestsIdentity;

namespace com.softpine.muvany.models.Requests;

/// <summary>
/// 
/// </summary>
public class DeleteRolePermissionsRequest
{
    /// <summary>
    /// 
    /// </summary>
    public string RoleId { get; set; } = default!;
    /// <summary>
    /// 
    /// </summary>
    public List<PermissionsRequest> Permisos { get; set; } = default!;
}


