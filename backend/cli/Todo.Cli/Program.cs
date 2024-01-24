using Todo.Cli;
using Todo.Cli.Menu;
using Todo.Cli.Menu.Actions;

ToDoRepository repository = new ToDoRepository();

var options = new List<Option>
{
    new Option("View all incompleted tasks", () => new ListIncompleteItemsAction(repository).Run()),
    new Option("View all completed tasks", () => new ListCompleteItemsAction(repository).Run()),
    new Option("Add a new task", () => new AddNewItemAction(repository).Run()),
    new Option("Mark task as completed", () => new CompleteItemAction(repository).Run()),
    new Option("Remove a task", () => new RemoveItemAction(repository).Run()),
    new Option("Exit", () => Environment.Exit(0)),
};

new MenuNavigation(options).Start();