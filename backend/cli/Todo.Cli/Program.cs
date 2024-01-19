using Todo.Cli.Menu;
using Todo.Cli.Menu.Actions;

MenuNavigation menu = new MenuNavigation();

menu.Start();

public class MenuOptions : MenuNavigation
{
    public static List<Option>? Options = new List<Option>
    {
        new Option("View all incompleted tasks", () => new ListIncompleteItemsAction(Items).Run()),
        new Option("View all completed tasks", () => new ListCompleteItemsAction(Items).Run()),
        new Option("Add a new task", () => new AddNewItemAction(Items).Run()),
        new Option("Mark task as completed", () => new CompleteItemAction(Items).Run()),
        new Option("Remove a task", () => new RemoveItemAction(Items).Run()),
        new Option("Exit", () => Environment.Exit(0)),
    };
}