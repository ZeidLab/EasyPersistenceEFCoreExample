using ZeidLab.ToolBox.EasyPersistence.EFCore;

namespace ExampleTodoListProject.Data.Entities;

public class TodoCategory : EntityBase<Guid>, IAggregateRoot
{
    public string Title { get; set; }
    public virtual ICollection<TodoItem> TodoItems { get; set; }
}