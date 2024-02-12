namespace Todo.Cli.Menu.Actions;

public class ListIncompleteItemsAction : IMenuAction
{
    private readonly ToDoRepository _repository;

    public ListIncompleteItemsAction(ToDoRepository repository)
    {
        _repository = repository;
    }

    public async void Run()
    {
        var incompleteTasks = await _repository.ListIncompleteTasks();
        
        Console.WriteLine("Current incomplete tasks:");
        
        foreach (var item in incompleteTasks.Data)
        {
            Console.WriteLine(item.Name);
            Console.WriteLine(item.IsComplete);
            Console.WriteLine(item.CreatedAt);
            Console.WriteLine(item.Id);
        }
    }
}