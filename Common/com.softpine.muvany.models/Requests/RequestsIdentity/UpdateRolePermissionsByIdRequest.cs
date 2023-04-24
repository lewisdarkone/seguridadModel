
namespace com.softpine.muvany.models.Requests;

/// <summary>
/// 
/// </summary>
public class UpdateRolePermissionsByIdRequest
{
    /// <summary>
    /// 
    /// </summary>
    public string RoleId { get; set; } = default!;
    /// <summary>
    /// 
    /// </summary>
    public List<int> Permissions { get; set; } = default!;
}


