using ExampleTodoListProject.Data.Interfaces;
using ZeidLab.ToolBox.EasyPersistence.EFCore;

namespace ExampleTodoListProject.Data;

public interface IUnitOfWork : IUnitOfWorkBase
{
    ITodoCategoryRepository TodoCategoryRepository { get; }
}