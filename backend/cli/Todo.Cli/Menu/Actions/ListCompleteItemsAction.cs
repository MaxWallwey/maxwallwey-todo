namespace Todo.Cli.Menu.Actions;

public class ListCompleteItemsAction : IMenuAction
{
    private readonly List<Todo> _items;

    public ListCompleteItemsAction(List<Todo> items)
    {
        _items = items;
    }

    public void Run()
    {
        Console.WriteLine("Current completed tasks:");

        foreach (var item in _items.Where(item => item.IsComplete == true))
        {
            Console.WriteLine(item.Name);
        }
    }
}