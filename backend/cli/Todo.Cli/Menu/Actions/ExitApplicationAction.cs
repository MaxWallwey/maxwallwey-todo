namespace Todo.Cli.Menu.Actions;

public class ExitApplicationAction : IMenuAction
{
    private readonly List<Todo> _items;

    public ExitApplicationAction(List<Todo> items)
    {
        _items = items;
    }

    public void Run()
    {
        System.Environment.Exit(0);
    }
}