using MediatR;
using Microsoft.Extensions.Logging;
using com.softpine.muvany.core.Events;
using com.softpine.muvany.models.Interfaces;

namespace com.softpine.muvany.infrastructure.Helpers;

/// <summary>
/// 
/// </summary>
public class EventPublisher : IEventPublisher
{
    private readonly ILogger<EventPublisher> _logger;
    private readonly IPublisher _mediator;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public EventPublisher(ILogger<EventPublisher> logger, IPublisher mediator) =>
        (_logger, _mediator) = (logger, mediator);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="event"></param>
    /// <returns></returns>
    public Task PublishAsync(IEvent @event)
    {
        _logger.LogInformation("Publishing Event : {event}", @event.GetType().Name);
        return _mediator.Publish(CreateEventNotification(@event));
    }

    private INotification CreateEventNotification(IEvent @event) =>
        (INotification)Activator.CreateInstance(
            typeof(EventNotification<>).MakeGenericType(@event.GetType()), @event)!;
}
