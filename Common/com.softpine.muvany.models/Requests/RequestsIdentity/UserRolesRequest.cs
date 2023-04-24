using System.ComponentModel.DataAnnotations;

namespace com.softpine.muvany.models.Requests;

/// <summary>
/// 
/// </summary>
public class UserRolesRequest
{
    /// <summary>
    /// 
    /// </summary>
    [Required(ErrorMessage = "UserRoles es un campo obligatorio.")]
    [MinLength(1, ErrorMessage = "Debe de seleccionar por lo menos 1 rol.")]
    public List<RolesIds> UserRoles { get; set; } = new();
}

/// <summary>
/// 
/// </summary>
public class RolesIds
{
    /// <summary>
    /// 
    /// </summary>
    public string RoleId { get; set; }
}
