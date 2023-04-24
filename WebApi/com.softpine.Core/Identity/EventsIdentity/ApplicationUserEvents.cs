using com.softpine.muvany.models.Contracts;

namespace com.softpine.muvany.models.Interfaces;

/// <summary>
/// 
/// </summary>
public abstract class ApplicationUserEvent : DomainEvent
{
    /// <summary>
    /// 
    /// </summary>
    public string UserId { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    protected ApplicationUserEvent(string userId) => UserId = userId;
}

/// <summary>
/// 
/// </summary>
public class ApplicationUserCreatedEvent : ApplicationUserEvent
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    public ApplicationUserCreatedEvent(string userId)
        : base(userId)
    {
    }
}

/// <summary>
/// 
/// </summary>
public class ApplicationUserUpdatedEvent : ApplicationUserEvent
{
    /// <summary>
    /// 
    /// </summary>
    public bool RolesUpdated { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="rolesUpdated"></param>
    public ApplicationUserUpdatedEvent(string userId, bool rolesUpdated = false)
        : base(userId) =>
        RolesUpdated = rolesUpdated;
}
