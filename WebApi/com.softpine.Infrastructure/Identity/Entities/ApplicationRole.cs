using Microsoft.AspNetCore.Identity;

namespace com.softpine.muvany.infrastructure.Identity.Entities;

/// <summary>
/// 
/// </summary>
public class ApplicationRole : IdentityRole
{
    /// <summary>
    /// 
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int? TypeRol { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    public ApplicationRole(string name, string? description = null, int? typeRol = 0)
        : base(name)
    {
        Description = description;
        NormalizedName = name.ToUpperInvariant();
        TypeRol = typeRol;
    }


}
