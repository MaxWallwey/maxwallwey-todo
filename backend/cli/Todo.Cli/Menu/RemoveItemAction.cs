namespace Todo.Cli.Menu;

public class RemoveItemAction : IMenuAction
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

        var itemToRemove = _items.FirstOrDefault(i => i.Name == removeTask);

        if (itemToRemove != null && removeTask != "exit")
        {
            _items.Remove(itemToRemove);
        }
    }
}