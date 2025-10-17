using ExampleTodoListProject.Data.Entities;

namespace ExampleTodoListProject.Data.Dtos
{
    //public record KanBanSectionDto();

    public readonly record struct KanBanSectionDto(Guid Id, string Name, IList<KanbanTaskItemDto> Items);
}