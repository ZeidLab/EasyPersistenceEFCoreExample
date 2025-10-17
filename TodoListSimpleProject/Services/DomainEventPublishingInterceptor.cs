using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ZeidLab.ToolBox.EasyPersistence.EFCore;
using ZeidLab.ToolBox.EventBuss;

namespace ExampleTodoListProject.Services;
/// <summary>
/// Intercepts the save changes operation to publish domain events after changes are successfully saved to the database.
/// </summary>
/// <remarks>This interceptor listens for entities implementing the <see cref="IHaveDomainEvents"/> interface and
/// publishes their domain events using the provided <see cref="IEventBussService"/>. After the events are published,
/// the domain event collection is cleared.</remarks>
public sealed class DomainEventPublishingInterceptor : SaveChangesInterceptor
{
    private readonly IEventBussService _eventBuss;

    public DomainEventPublishingInterceptor(IEventBussService eventBussService)
    {
        _eventBuss = eventBussService;
    }

    /// <summary>
    /// Invokes domain event handlers for entities with domain events after changes are saved asynchronously.
    /// </summary>
    /// <remarks>This method processes domain events for entities implementing <see cref="IHaveDomainEvents"/>
    /// after the save operation is completed. Each domain event is dispatched to its corresponding handler, and the
    /// domain events are cleared from the entity after processing.</remarks>
    /// <param name="eventData">The event data containing the context of the save operation.</param>
    /// <param name="result">The result of the save operation, representing the number of state entries written to the database.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation. The result contains the number of
    /// state entries written to the database.</returns>
    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var context = eventData.Context;
        if (context is null) return result;

        var entities = context.ChangeTracker.Entries<IHaveDomainEvents>()
            .Where(e => e.Entity.DomainEvents.Count > 0)
            .Select(e => e.Entity)
            .ToList();
        foreach (var entity in entities)
        {
            foreach (var domainEvent in entity.DomainEvents.Cast<IDomainEventInvoker>())
            {
                await domainEvent.InvokeHandlerAsync(_eventBuss, cancellationToken);
            }
            entity.DomainEvents.Clear();
        }
        return result;
    }
}