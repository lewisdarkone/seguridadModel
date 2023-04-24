using System.Linq;

namespace com.softpine.muvany.models.DTOS;

/// <summary>
/// Clase para representar las propiedades de los usuarios
/// </summary>
public class UserDetailsDto
{
    /// <summary>
    /// 
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool EmailConfirmed { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? NombreCompleto { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<UserRoleDto>? Roles { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DateTime? ExpirePasswordDate { get; set; }
    /// <summary>
    /// Para habilitar doble factor de autenticación
    /// </summary>
    public bool TwoFactorEnable { get; set; }
    public string RolesToString()
    {
        if(Roles == null) return string.Empty;
        return string.Join(", ",Roles);
    }
}
