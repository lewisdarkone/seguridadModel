namespace com.softpine.muvany.models.Contracts;

/// <summary>
/// 
/// </summary>
public interface IEntity
{
    /// <summary>
    /// 
    /// </summary>
    List<DomainEvent> DomainEvents { get; }
}

