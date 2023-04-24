namespace com.softpine.muvany.models.Contracts;

// Apply this marker interface only to aggregate root entities
// Repositories will only work with aggregate roots, not their children
/// <summary>
/// 
/// </summary>
public interface IAggregateRoot : IEntity
{
}
