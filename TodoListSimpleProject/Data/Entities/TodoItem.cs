using ZeidLab.ToolBox.EasyPersistence.EFCore;

namespace ExampleTodoListProject.Data.Entities;

public class TodoItem: EntityBase<Guid>, IAggregateRoot
{
    public string Title { get; set; }
    public Guid CategoryId { get; set; }
    public TodoCategory Category { get; set; }
}