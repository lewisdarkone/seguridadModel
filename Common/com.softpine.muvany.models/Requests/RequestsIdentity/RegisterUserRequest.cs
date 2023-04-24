
namespace com.softpine.muvany.models.Requests;

/// <summary>
/// Formulario para register un usuario
/// </summary>
public class RegisterUserRequest
{
    /// <summary>
    /// Nombre completo (100 caracteres)
    /// </summary>
    public string FullName { get; set; } = default!;
    /// <summary>
    /// Correo electronico
    /// </summary>
    public string Email { get; set; } = default!;
}
