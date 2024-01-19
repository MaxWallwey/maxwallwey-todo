namespace Todo.Cli.Menu.Actions;

public class ListIncompleteItemsAction : ToDoRepository
{
    private readonly List<Todo> _items;

    public ListIncompleteItemsAction(List<Todo> items)
    {
        _items = items;
    }

    public void Run()
    {
        Console.WriteLine("Current incompleted tasks:");

        ListIncompleteTasks();
    }
}