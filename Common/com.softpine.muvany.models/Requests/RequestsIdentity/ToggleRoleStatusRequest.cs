namespace com.softpine.muvany.models.Requests;

/// <summary>
/// 
/// </summary>
public class ToggleRoleStatusRequest
{
    /// <summary>
    /// Valor boolean False ó True
    /// </summary>
    public bool? ActivateUser { get; set; }
    /// <summary>
    /// Número o Sucursal 
    /// </summary>
    public string? Id { get; set; }
}
