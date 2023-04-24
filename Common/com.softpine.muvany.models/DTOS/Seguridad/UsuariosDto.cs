#pragma warning disable 1591


namespace com.softpine.muvany.models.DTOS;

public class UsuariosDto
{

    public string? Id { get; set; }
    public string? UserName { get; set; }
    public string? NormalizedUserName { get; set; }
    public string? Email { get; set; }
    public string? NormalizedEmail { get; set; }
    public bool EmailConfirmed { get; set; }
    public string? PasswordHash { get; set; }
    public string? SecurityStamp { get; set; }
    public string? ConcurrencyStamp { get; set; }
    public string? PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
    public bool LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }
    public string? NombreCompleto { get; set; }
    public string? TipoIdentificacion { get; set; }
    public string? Identificacion { get; set; }
    public int? Estado { get; set; }
    public string? ImagenUrl { get; set; }
    public int? IdEmpleado { get; set; }
    public int? IdSupervisor { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public string? ObjetoId { get; set; }
    public int? IdOficial { get; set; }

}
