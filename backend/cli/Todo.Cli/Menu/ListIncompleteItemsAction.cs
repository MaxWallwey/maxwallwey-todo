namespace Todo.Cli.Menu;

public class ListIncompleteItemsAction : IMenuAction
{
    private readonly List<Todo> _items;

    public ListIncompleteItemsAction(List<Todo> items)
    {
        _items = items;
    }

    public void Run()
    {
        Console.WriteLine("Current incompleted tasks:");

        foreach (var item in _items.Where(item => item.IsComplete == false))
        {
            Console.WriteLine(item.Name);
        }
    }
}