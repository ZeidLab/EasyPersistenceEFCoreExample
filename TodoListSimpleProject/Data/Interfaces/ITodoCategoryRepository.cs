using ExampleTodoListProject.Data.Dtos;
using ExampleTodoListProject.Data.Entities;
using ZeidLab.ToolBox.Common;
using ZeidLab.ToolBox.EasyPersistence.EFCore;
using ZeidLab.ToolBox.Results;

namespace ExampleTodoListProject.Data.Interfaces;

public interface ITodoCategoryRepository : IRepositoryBase<TodoCategory, Guid>
{
    TryAsync<List<KanBanSectionDto>> GetAllCategoriesWithTheirTasks();
    Task<Result<Unit>> AddTodoItemToCategoryAsync(Guid sectionId, string title);
}