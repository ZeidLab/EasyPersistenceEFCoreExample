using ZeidLab.ToolBox.EventBuss;

namespace ExampleTodoListProject.Data.Evenets
{
    public readonly record struct SectionIsCreated(Guid Id ,string Name) : IAppEvent;
}