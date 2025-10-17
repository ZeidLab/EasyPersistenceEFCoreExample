using ZeidLab.ToolBox.EasyPersistence.EFCore;
using ZeidLab.ToolBox.EventBuss;

namespace ExampleTodoListProject.Services
{
    /// <summary>
    /// Wraps a domain event of type <typeparamref name="TEvent"/> to provide additional functionality for invoking
    /// event handlers and publishing the event without using reflection.
    /// </summary>
    /// <remarks>This class acts as a wrapper for domain events, enabling them to be invoked through an <see
    /// cref="IDomainEventInvoker"/> and published using an <see cref="IEventBussService"/>.</remarks>
    /// <typeparam name="TEvent">The type of the domain event being wrapped. Must implement <see cref="IAppEvent"/>.</typeparam>
    public class DomainEventWrapper<TEvent> : IDomainEventInvoker, IDomainEvent
        where TEvent : IAppEvent
    {
        /// <summary>
        /// Represents the domain event associated with this instance.
        /// </summary>
        /// <remarks>This field is read-only and holds the specific domain event of type <typeparamref
        /// name="TEvent"/>. It is intended to encapsulate event-related data within the domain model.</remarks>
        private readonly TEvent _domainEvent;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEventWrapper{TEvent}"/> class with the specified domain
        /// event.
        /// </summary>
        /// <param name="domainEvent">The domain event to wrap. Cannot be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="domainEvent"/> is <see langword="null"/>.</exception>
        public DomainEventWrapper(in TEvent domainEvent)
        {
            _domainEvent = domainEvent ?? throw new ArgumentNullException(nameof(domainEvent));
        }

        /// <summary>
        /// Invokes the specified event bus service to publish a domain event asynchronously.
        /// </summary>
        /// <param name="eventBussService">The event bus service used to publish the domain event. Cannot be <see langword="null"/>.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests. If cancellation is requested, the operation will terminate
        /// immediately.</param>
        /// <returns>A task that represents the asynchronous operation. The task is completed when the domain event is published.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="eventBussService"/> is <see langword="null"/>.</exception>
        public Task InvokeHandlerAsync(IEventBussService eventBussService, CancellationToken cancellationToken)
        {
            if (eventBussService == null) throw new ArgumentNullException(nameof(eventBussService));
            if (cancellationToken.IsCancellationRequested)
                return Task.FromCanceled(cancellationToken);

            eventBussService.Publish(_domainEvent);
            return Task.CompletedTask;
        }
    }
}