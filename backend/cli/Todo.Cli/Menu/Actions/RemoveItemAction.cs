namespace Todo.Cli.Menu.Actions;

public class RemoveItemAction : ToDoRepository
{
    private readonly List<Todo> _items;

    public RemoveItemAction(List<Todo> items)
    {
        _items = items;
    }

    public void Run()
    {
        Console.WriteLine("What task would you like to remove? To cancel this, type 'exit'\n");
        var removeTask = Console.ReadLine();

        RemoveTask(removeTask);
    }
}