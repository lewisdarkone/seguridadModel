using com.softpine.muvany.models.Entities.Subscripcion;

namespace com.softpine.muvany.models.DTOS;

/// <summary>
/// 
/// </summary>
public class RoleDto
{
    /// <summary>
    /// 
    /// </summary>
    public string Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? Description { get; set; }
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
        return Name;
    }
}
