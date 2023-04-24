
using com.softpine.muvany.core.Events;
using MediatR;

namespace com.softpine.muvany.models.Interfaces;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TEvent"></typeparam>
public class EventNotification<TEvent> : INotification
    where TEvent : IEvent
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="event"></param>
    public EventNotification(TEvent @event) => Event = @event;

    /// <summary>
    /// 
    /// </summary>
    public TEvent Event { get; }
}
