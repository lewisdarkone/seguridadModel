using System.Xml.Linq;

namespace com.softpine.muvany.models.DTOS;

/// <summary>
/// 
/// </summary>
public class UserRoleDto
{
    /// <summary>
    /// 
    /// </summary>
    public string? RoleId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? RoleName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool? Enabled { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int? TypeRol { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? TypeRolDescription { get; set; }

    public override string ToString()
    {
        return RoleName;
    }
}
