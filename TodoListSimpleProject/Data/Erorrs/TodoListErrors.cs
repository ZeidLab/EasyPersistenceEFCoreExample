using ZeidLab.ToolBox.Results;

namespace ExampleTodoListProject.Data.Erorrs
{
    public static class TodoListErrors
    {
        public static ResultError ThereAreNoRecords = ResultError.New("There is no records to display");
    }
}