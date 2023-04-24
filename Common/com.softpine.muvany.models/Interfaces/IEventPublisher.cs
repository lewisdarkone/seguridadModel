using com.softpine.muvany.models.Interfaces;

namespace com.softpine.muvany.core.Events;

/// <summary>
/// 
/// </summary>
public interface IEventPublisher : ITransientService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="event"></param>
    /// <returns></returns>
    Task PublishAsync(IEvent @event);
}
