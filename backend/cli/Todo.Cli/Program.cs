using Todo.Cli;
using Todo.Cli.Menu;
using Todo.Cli.Menu.Actions;

ToDoRepository repository = new ToDoRepository(new List<Todo.Cli.ToDo>{
    new Todo.Cli.ToDo("Buy some milk"), 
    new Todo.Cli.ToDo("Call the dentist"), 
    new Todo.Cli.ToDo("Cancel Netflix", true)});

// Instantiate actions for saving memory
var incompleteItemsAction = new ListIncompleteItemsAction(repository);
var completeItemsAction = new ListCompleteItemsAction(repository);
var addItemsAction = new AddNewItemAction(repository);
var markCompleteItemsAction = new CompleteItemAction(repository);
var removeItemsAction = new RemoveItemAction(repository);

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