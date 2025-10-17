using ExampleTodoListProject.Data;
using ExampleTodoListProject.Data.Dtos;
using ExampleTodoListProject.Data.Erorrs;
using Microsoft.EntityFrameworkCore;
using ZeidLab.ToolBox.Common;
using ZeidLab.ToolBox.Results;

namespace ExampleTodoListProject.Services;

public sealed class TodoListService : ITodoListService
{
    private readonly IUnitOfWork _unitOfWork;

    public TodoListService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<KanBanSectionDto>>> GetSectionsWithItemsAsync()
    {
        // get all categories with their tasks
        var sections = await _unitOfWork
            .TodoCategoryRepository
            // This only returns a method that either returns a list of categories with their tasks or an exception as error
            .GetAllCategoriesWithTheirTasks()
            // This executes the method and returns the result or the exception as an error
            .TryAsync();

        return sections;
    }

    public async Task<Result<Unit>> AddTodoItemAsync(NewTaskItemDto newTask)
    {

        if (string.IsNullOrWhiteSpace(newTask.Content))
            return ResultError.New("Task content cannot be empty");

        return await _unitOfWork.TodoCategoryRepository
            .AddTodoItemToCategoryAsync(newTask.SectionId, newTask.Content);
        //.FindFirstOrDefaultAsync(x => x.Id == newTask.SectionId);
        
    }
}