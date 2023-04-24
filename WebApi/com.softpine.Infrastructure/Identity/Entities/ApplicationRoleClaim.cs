using Microsoft.AspNetCore.Identity;

namespace com.softpine.muvany.infrastructure.Identity.Entities;

/// <summary>
/// 
/// </summary>
public class ApplicationRoleClaim : IdentityRoleClaim<string>
{
    /// <summary>
    /// 
    /// </summary>
    public int? PermisoId { get; set; }
}
