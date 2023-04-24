using Microsoft.AspNetCore.Identity;

namespace com.softpine.muvany.infrastructure.Identity.Entities;

/// <summary>
/// 
/// </summary>
public class ApplicationUser : IdentityUser
{

    /// <summary>
    /// 
    /// </summary>
    public string? NombreCompleto { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? TipoIdentificacion { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? Identificacion { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int? Estado { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? ImagenUrl { get; set; }
    /// <summary>
    /// Indica fecha de expiración de una contraseña para un usuario externo 
    /// </summary>
    public DateTime? ExpirePasswordDate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int? IdEmpleado { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? RefreshToken { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DateTime? RefreshTokenExpiryTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? ObjetoId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? TempCode { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DateTime? ExpireTempCodeDate { get; set; }
    /// <summary>
    /// Habilitado cuando se desea que el usuario se autentique por doble factor
    /// </summary>
    public bool TwoFactorEnabled { get; set; }
}
