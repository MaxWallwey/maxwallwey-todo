using Todo.Cli;
using Todo.Cli.Menu;
using Todo.Cli.Menu.Actions;

// Instantiate actions for saving memory
var incompleteItemsAction = new ListIncompleteItemsAction();
var completeItemsAction = new ListCompleteItemsAction();
var addItemsAction = new AddNewItemAction();
var markCompleteItemsAction = new CompleteItemAction();
var removeItemsAction = new RemoveItemAction();

// Instantiate options for menu
var options = new List<Option>
{
    new Option("View all incompleted tasks", () => incompleteItemsAction.Run()),
    new Option("View all completed tasks", () => completeItemsAction.Run()),
    new Option("Add a new task", () => addItemsAction.Run()),
    new Option("Mark task as completed", () => markCompleteItemsAction.Run()),
    new Option("Remove a task", () => removeItemsAction.Run()),
    new Option("Exit", () => new ExitApplicationAction().Run()),
};

//Start Menu
new MenuNavigation(options).Start();