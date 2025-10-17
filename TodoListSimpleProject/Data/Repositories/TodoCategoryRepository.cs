using ExampleTodoListProject.Data.Dtos;
using ExampleTodoListProject.Data.Entities;
using ExampleTodoListProject.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ZeidLab.ToolBox.Common;
using ZeidLab.ToolBox.EasyPersistence.EFCore;
using ZeidLab.ToolBox.Results;

namespace ExampleTodoListProject.Data.Repositories;

internal sealed class TodoCategoryRepository : RepositoryBase<TodoCategory, Guid>, ITodoCategoryRepository
{
    private readonly ApplicationDbContext _appDbContext;

    public TodoCategoryRepository(ApplicationDbContext context) : base(context)
    {
        _appDbContext = context;
    }

    public TryAsync<List<KanBanSectionDto>> GetAllCategoriesWithTheirTasks()
    {

        return async () =>
        {
            var data = await _appDbContext.TodoCategories
                .AsNoTracking()
                .Select(cat => new KanBanSectionDto(cat.Id, cat.Title,
                    cat.TodoItems
                        .Select(item => new KanbanTaskItemDto(item.Id, item.Title, item.CategoryId))
                        .ToList()
                ))
                .ToListAsync();
            return data;
        };

    }

    public async Task<Result<Unit>> AddTodoItemToCategoryAsync(Guid sectionId, string title)
    {
        try
        {
           var category = _appDbContext.TodoCategories.
                Include(x => x.TodoItems)
                .FirstOrDefault(x => x.Id == sectionId);

            if (category is null)
                return ResultError.New("The specified category does not exist");

            var newTodoItem = category.AddTodoItem(title);

            _appDbContext.Add(newTodoItem);

            var totalSaved = await _appDbContext.SaveChangesAsync();

            if (totalSaved == 0)
                return ResultError.New("No record is saved");

            return Result.Success(Unit.Default);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            // Handle concurrency conflict specifically
            return ResultError.New("The category was modified by another user. Please refresh and try again.", ex);
        }
        catch (Exception e)
        {
            return ResultError
                .New("There was an error adding new task", e);
        }
    }
}