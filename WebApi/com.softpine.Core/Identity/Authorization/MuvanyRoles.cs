using System.Collections.ObjectModel;

namespace com.softpine.muvany.core.Authorization;

/// <summary>
/// 
/// </summary>
public static class MuvanyRoles
{
    /// <summary>
    /// 
    /// </summary>
    public const string Administrador = nameof(Administrador);

    /// <summary>
    /// 
    /// </summary>
    public static IReadOnlyList<string> DefaultRoles { get; } = new ReadOnlyCollection<string>(new[]
    {
        Administrador
    });

    /// <summary>
    /// 
    /// </summary>
    /// <param name="roleName"></param>
    /// <returns></returns>
    public static bool IsDefault(string roleName) => DefaultRoles.Any(r => r == roleName);
}
