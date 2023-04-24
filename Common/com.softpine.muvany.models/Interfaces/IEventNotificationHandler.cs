using com.softpine.muvany.models.Interfaces;
using MediatR;

namespace com.softpine.muvany.core.Events;

// This is just a shorthand to make it a bit easier to create event handlers for specific events.
/// <summary>
/// 
/// </summary>
/// <typeparam name="TEvent"></typeparam>
public interface IEventNotificationHandler<TEvent> : INotificationHandler<EventNotification<TEvent>>
    where TEvent : IEvent
{
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="TEvent"></typeparam>
public abstract class EventNotificationHandler<TEvent> : INotificationHandler<EventNotification<TEvent>>
    where TEvent : IEvent
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task Handle(EventNotification<TEvent> notification, CancellationToken cancellationToken) =>
        Handle(notification.Event, cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="event"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task Handle(TEvent @event, CancellationToken cancellationToken);
}
