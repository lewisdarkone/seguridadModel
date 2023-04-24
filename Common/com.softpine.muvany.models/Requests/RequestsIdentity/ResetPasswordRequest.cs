namespace com.softpine.muvany.models.Requests;

/// <summary>
/// 
/// </summary>
public class ResetPasswordRequest
{
    /// <summary>
    /// Correo electronico
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Contraseña
    /// </summary>
    public string Password { get; set; } = default!;

    /// <summary>
    /// Contraseña
    /// </summary>
    public string ConfirmPassword { get; set; } = default!;

    /// <summary>
    /// Código
    /// </summary>
    public string CodigoValidacion { get; set; } = default!;
}
