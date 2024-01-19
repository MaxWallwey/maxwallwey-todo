namespace Todo.Cli.Menu.Actions;

public class CompleteItemAction : ToDoRepository
{
    private readonly List<Todo> _items;

    public CompleteItemAction(List<Todo> items)
    {
        _items = items;
    }

    public void Run()
    {
        Console.WriteLine("What task would you like to mark as completed? To cancel this, type 'exit'\n");
        var completeTask = Console.ReadLine();

        CompleteTask(completeTask);
    }
}