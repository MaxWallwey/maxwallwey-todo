namespace Todo.Cli.Menu;

public class AddNewItemAction : IMenuAction
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

        if (newTask != null && newTask != "exit")
        {
            _items.Add(new Todo(newTask));
        }
    }
}