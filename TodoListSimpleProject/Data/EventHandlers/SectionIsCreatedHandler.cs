using ExampleTodoListProject.Data.Evenets;
using ZeidLab.ToolBox.EventBuss;

namespace ExampleTodoListProject.Data.EventHandlers
{
    public class SectionIsCreatedHandler : IAppEventHandler<SectionIsCreated>
    {
        private readonly ILogger<SectionIsCreatedHandler> _logger;

        public SectionIsCreatedHandler(ILogger<SectionIsCreatedHandler> logger)
        {
            _logger = logger;
        }
        public async Task HandleAsync(SectionIsCreated appEvent, CancellationToken cancellationToken = new CancellationToken())
        {
            await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            _logger.LogInformation("SectionIsCreatedHandler: Section with Id {Id} and Name {Name} is created.", appEvent.Id, appEvent.Name);
        }
    }
}