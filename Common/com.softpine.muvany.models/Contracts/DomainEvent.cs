using com.softpine.muvany.core.Events;

namespace com.softpine.muvany.models.Contracts;

/// <summary>
/// 
/// </summary>
public abstract class DomainEvent : IEvent
{
    /// <summary>
    /// 
    /// </summary>
    public DateTime TriggeredOn { get; protected set; } = DateTime.UtcNow;
}
