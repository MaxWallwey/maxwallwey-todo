namespace Todo.Cli.Menu.Actions;

public class AddNewItemAction : ToDoRepository
{
    private readonly List<Todo> _items;

    public AddNewItemAction(List<Todo> items)
    {
        _items = items;
    }

    public void Run()
    {
        Console.WriteLine("What task would you like to add? To cancel this, type 'exit'\n");
        string? newTask = Console.ReadLine();

        AddTask(newTask);
    }
}