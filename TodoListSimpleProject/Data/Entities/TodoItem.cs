using ZeidLab.ToolBox.EasyPersistence.EFCore;

namespace ExampleTodoListProject.Data.Entities;

public class TodoItem: EntityBase<Guid> , IHaveDomainEvents
{
    public string Title { get; private set; }
    public Guid CategoryId { get; private set; }
    public virtual TodoCategory Category { get; private set; }
    // EF Core requires an empty constructor for materialization
    private TodoItem()
    {
        
    }

    private TodoItem(string title, TodoCategory category):base(Guid.CreateVersion7())
    {
        Title = title;
        Category = category;
    }

    public static TodoItem Create(string title, TodoCategory categoryId)
    {
        // You can add validation or other logic here if needed
        var todoItem = new TodoItem(title, categoryId);


        return todoItem;
    }

    public void UpdateTitle(string newTitle)
    {
        // You can add validation or other logic here if needed
        Title = newTitle;
    }

    public void ChangeCategory(TodoCategory newCategory)
    {
        // You can add validation or other logic here if needed
        Category = newCategory;
    }
}