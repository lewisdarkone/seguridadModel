namespace com.softpine.muvany.models.Requests;

/// <summary>
/// 
/// </summary>
public class UpdateUserProfileRequest
{
    /// <summary>
    /// 
    /// </summary>
    public string Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string PhoneNumber { get; set; }
    /// <summary>
    /// Para indicar si desea habilitar el doble factor de autenticación
    /// </summary>
    public bool TwoFactorEnable { get; set; }
}







