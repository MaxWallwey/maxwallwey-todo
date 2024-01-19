namespace Todo.Cli.Menu.Actions;

public class ListCompleteItemsAction : ToDoRepository
{
    private readonly List<Todo> _items;

    public ListCompleteItemsAction(List<Todo> items)
    {
        _items = items;
    }

    public void Run()
    {
        Console.WriteLine("Current completed tasks:");

        ListCompleteTasks();
    }
}