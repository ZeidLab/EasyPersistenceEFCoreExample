using ExampleTodoListProject.Data.Dtos;
using ZeidLab.ToolBox.Common;
using ZeidLab.ToolBox.Results;

namespace ExampleTodoListProject.Services;

public interface ITodoListService
{
    Task<Result<List<KanBanSectionDto>>> GetSectionsWithItemsAsync();
    Task<Result<Unit>> AddTodoItemAsync(NewTaskItemDto newTask);
}