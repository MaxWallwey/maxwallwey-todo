namespace Todo.Cli.Menu.Actions;

public class ListCompleteItemsAction : IMenuAction
{
    private readonly ToDoRepository _repository;

    public ListCompleteItemsAction(ToDoRepository repository)
    {
        _repository = repository;
    }

    public void Run()
    {
        Console.WriteLine("Current completed tasks:");

        foreach (var item in _repository.ListCompleteTasks())
        {
            Console.WriteLine(item.Name);
        }
    }
}