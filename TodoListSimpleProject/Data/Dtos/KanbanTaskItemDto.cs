namespace ExampleTodoListProject.Data.Dtos
{
    public readonly record struct KanbanTaskItemDto(Guid Id, string Content, Guid SectionId);
    public readonly record struct NewTaskItemDto(string Content, Guid SectionId);
}