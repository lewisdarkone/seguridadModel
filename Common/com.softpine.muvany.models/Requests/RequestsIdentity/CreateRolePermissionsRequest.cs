using com.softpine.muvany.models.Request.RequestsIdentity;

namespace com.softpine.muvany.models.Requests;

/// <summary>
/// 
/// </summary>
public class CreateRolePermissionsRequest
{
    /// <summary>
    /// 
    /// </summary>
    public string RoleId { get; set; } = default!;
    /// <summary>
    /// 
    /// </summary>
    public List<PermissionsRequest> Permissions { get; set; } = default!;
}


