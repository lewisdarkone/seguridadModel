using Microsoft.EntityFrameworkCore;
using com.softpine.muvany.core.Events;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.Contracts;

namespace com.softpine.muvany.infrastructure.Persistence.EFContexts
{
    /// <summary>
    /// Clase parcial para adecuaciones personalizadas para manejo de auditorías
    /// </summary>
    public partial class LenderesContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly ICurrentUser _currentUser;
        private readonly ISerializerService _serializer;
        private readonly IEventPublisher _events;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="currentUser"></param>
        /// <param name="serializer"></param>
        /// <param name="events"></param>
        public LenderesContext(DbContextOptions<LenderesContext> options, ICurrentUser currentUser, ISerializerService serializer, IEventPublisher events)
            : base(options)
        {
            _currentUser = currentUser;
            _serializer = serializer;
            _events = events;
        }
        

        private async Task SendDomainEventsAsync()
        {
            var entitiesWithEvents = ChangeTracker.Entries<IEntity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Count > 0)
                .ToArray();

            foreach ( var entity in entitiesWithEvents )
            {
                var domainEvents = entity.DomainEvents.ToArray();
                entity.DomainEvents.Clear();
                foreach ( var domainEvent in domainEvents )
                {
                    await _events.PublishAsync(domainEvent);
                }
            }
        }
    }
}
