namespace com.softpine.muvany.models.Requests;

/// <summary>
/// 
/// </summary>
public class UpdateUserRequest
{
    /// <summary>
    /// 
    /// </summary>
    public string Id { get; set; } = default!;
    /// <summary>
    /// 
    /// </summary>
    public string FullName { get; set; }
    public string Email { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string PhoneNumber { get; set; }
    /// <summary>
    /// Indicar si el usuario desea habilitar el doble factura de autenticación
    /// </summary>
    public bool TwoFactorEnable { get; set; }
    /// <summary>
    /// Indica fecha de expiración de una contraseña para un usuario externo 
    /// </summary>
    public DateTime? ExpirePasswordDate { get; set; }

    //public FileUploadRequest? Image { get; set; }
    //public bool DeleteCurrentImage { get; set; } = false;
}
