
namespace com.softpine.muvany.models.Requests;

/// <summary>
/// Formulario para crear un usuario
/// </summary>
public class CreateUserRequest
{
    /// <summary>
    /// Nombre completo (100 caracteres)
    /// </summary>
    public string FullName { get; set; } = default!;
    /// <summary>
    /// Correo electronico
    /// </summary>
    public string Email { get; set; } = default!;
    /// <summary>
    /// Id del Role
    /// </summary>
    public string RoleId { get; set; } = default!;
    /// <summary>
    /// Numero telefonico (10 numeros)
    /// </summary>
    public string? PhoneNumber { get; set; }
    /// <summary>
    /// Indica fecha de expiración de una contraseña para un usuario externo 
    /// </summary>
    public DateTime? ExpirePasswordDate { get; set; }
}
