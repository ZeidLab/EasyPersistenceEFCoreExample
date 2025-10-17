using ExampleTodoListProject.Data.Interfaces;
using ExampleTodoListProject.Data.Repositories;
using ZeidLab.ToolBox.EasyPersistence.EFCore;

namespace ExampleTodoListProject.Data;

internal sealed class UnitOfWork : UnitOfWorkBase<ApplicationDbContext>, IUnitOfWork
{
    public UnitOfWork(ApplicationDbContext context) : base(context)
    {
        TodoCategoryRepository = new TodoCategoryRepository(context);
        
    }

    public ITodoCategoryRepository TodoCategoryRepository { get; }
}