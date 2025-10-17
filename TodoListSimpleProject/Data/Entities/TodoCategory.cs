using ExampleTodoListProject.Data.Evenets;
using ExampleTodoListProject.Services;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ZeidLab.ToolBox.EasyPersistence.EFCore;

namespace ExampleTodoListProject.Data.Entities;
/// <summary>
/// This is a TodoCategory entity representing a category of todoitems.
/// Based on DDD principles, it is an aggregate root.
/// This aggregate root contains a collection of TodoItems.
/// The TodoItem is not accessible directly from outside the aggregate.
/// and does not have its own repository.
/// However, The TodoItem can be accessed through the ApplicationDbContext.
/// </summary>
public sealed class TodoCategory : EntityBase<Guid>, IAggregateRoot, IHaveDomainEvents
{
    internal const string EF_TODO_ITEMS_FIELD_NAME = nameof(_todoItems);
    public string Title { get; set; }
    private ICollection<TodoItem> _todoItems;

    // Add this concurrency token
    [Timestamp]
    public byte[] RowVersion { get; set; }

    public List<TodoItem> TodoItems => _todoItems.ToList();

    // EF Core requires an empty constructor for materialization
    private TodoCategory()
    {
        _todoItems = new List<TodoItem>();
    }
    private TodoCategory(string title) : base(Guid.CreateVersion7())
    {
        Title = title;
        _todoItems = new List<TodoItem>();
    }
    public static TodoCategory Create(string title)
    {
        // You can add validation or other logic here if needed
        var todoCategory = new TodoCategory(title);
        var sectionIsCreated = new SectionIsCreated(todoCategory.Id, todoCategory.Title);
        todoCategory.DomainEvents.Add(new DomainEventWrapper<SectionIsCreated>(sectionIsCreated));
        return todoCategory;
    }

    public void UpdateTitle(string newTitle)
    {
        // You can add validation or other logic here if needed
        Title = newTitle;
    }

    public TodoItem AddTodoItem(string Title)
    {
        // You can add validation or other logic here if needed
        var item = TodoItem.Create(Title, this);
        
        _todoItems.Add(item);

        return item;

    }
}