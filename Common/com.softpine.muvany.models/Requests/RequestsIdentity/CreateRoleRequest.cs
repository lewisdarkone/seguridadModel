namespace com.softpine.muvany.models.Requests;

/// <summary>
/// 
/// </summary>
public class CreateRoleRequest
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// 
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int? TypeRol { get; set; }
}


