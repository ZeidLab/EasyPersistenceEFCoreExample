using ZeidLab.ToolBox.EventBuss;

namespace ExampleTodoListProject.Services
{
    public interface IDomainEventInvoker
    {
        Task InvokeHandlerAsync(IEventBussService eventBussService, CancellationToken cancellationToken);
    }
}