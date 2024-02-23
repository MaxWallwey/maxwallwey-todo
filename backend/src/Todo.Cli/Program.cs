using Refit;
using ToDo.Api.Sdk;
using Todo.Cli.Menu;
using Todo.Cli.Menu.Actions;

// Instantiate actions for saving memory
var toDoClient = RestService.For<IToDoClient>("https://localhost:9000");

var incompleteItemsAction = new ListIncompleteItemsAction(toDoClient);
var completeItemsAction = new ListCompleteItemsAction(toDoClient);
var addItemsAction = new AddNewItemAction(toDoClient);
var markCompleteItemsAction = new CompleteItemAction(toDoClient);
var removeItemsAction = new RemoveItemAction(toDoClient);

// Instantiate options for menu
var options = new List<Option>
{
    new Option("View all incomplete tasks", async () => await incompleteItemsAction.Run()),
    new Option("View all completed tasks", async () => await completeItemsAction.Run()),
    new Option("Add a new task", async () => await addItemsAction.Run()),
    new Option("Mark task as completed", async () => await markCompleteItemsAction.Run()),
    new Option("Remove a task", async () => await removeItemsAction.Run()),
    new Option("Exit", async () => new ExitApplicationAction().Run()),
};

//Start Menu
new MenuNavigation(options).Start();