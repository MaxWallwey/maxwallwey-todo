namespace Todo.Cli.Menu.Actions;

public class ListIncompleteItemsAction : IMenuAction
{
    private readonly ToDoRepository _repository;

    public ListIncompleteItemsAction(ToDoRepository repository)
    {
        _repository = repository;
    }

    public void Run()
    {
        Console.WriteLine("Current incomplete tasks:");

        foreach (var item in _repository.ListIncompleteTasks())
        {
            Console.WriteLine(item.Name);
        }
    }
}