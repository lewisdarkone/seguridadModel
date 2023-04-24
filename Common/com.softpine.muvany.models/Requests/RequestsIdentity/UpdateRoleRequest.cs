namespace com.softpine.muvany.models.Requests;

/// <summary>
///  Clase para las propiedades a enviar 
/// </summary>
public class UpdateRoleRequest
{
    /// <summary>
    /// 
    /// </summary>
    public string? Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// 
    /// </summary>
    public string? Description { get; set; }
}
