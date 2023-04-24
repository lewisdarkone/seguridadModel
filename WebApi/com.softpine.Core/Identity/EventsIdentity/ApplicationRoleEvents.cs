using com.softpine.muvany.models.Contracts;

namespace com.softpine.muvany.models.Interfaces;

/// <summary>
/// 
/// </summary>
public abstract class ApplicationRoleEvent : DomainEvent
{
    /// <summary>
    /// 
    /// </summary>
    public string RoleId { get; set; } = default!;
    /// <summary>
    /// 
    /// </summary>
    public string RoleName { get; set; } = default!;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="roleName"></param>
    protected ApplicationRoleEvent(string roleId, string roleName) =>
        (RoleId, RoleName) = (roleId, roleName);
}

/// <summary>
/// 
/// </summary>
public class ApplicationRoleCreatedEvent : ApplicationRoleEvent
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="roleName"></param>
    public ApplicationRoleCreatedEvent(string roleId, string roleName)
        : base(roleId, roleName)
    {
    }
}

/// <summary>
/// 
/// </summary>
public class ApplicationRoleUpdatedEvent : ApplicationRoleEvent
{
    /// <summary>
    /// 
    /// </summary>
    public bool PermissionsUpdated { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="roleName"></param>
    /// <param name="permissionsUpdated"></param>
    public ApplicationRoleUpdatedEvent(string roleId, string roleName, bool permissionsUpdated = false)
        : base(roleId, roleName) =>
        PermissionsUpdated = permissionsUpdated;
}

/// <summary>
/// 
/// </summary>
public class ApplicationRoleDeletedEvent : ApplicationRoleEvent
{
    /// <summary>
    /// 
    /// </summary>
    public bool PermissionsUpdated { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="roleName"></param>
    public ApplicationRoleDeletedEvent(string roleId, string roleName)
        : base(roleId, roleName)
    {
    }
}
