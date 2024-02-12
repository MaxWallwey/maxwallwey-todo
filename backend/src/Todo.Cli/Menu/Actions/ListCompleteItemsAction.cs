namespace Todo.Cli.Menu.Actions;

public class ListCompleteItemsAction : IMenuAction
{
    private readonly ToDoRepository _repository;

    public ListCompleteItemsAction(ToDoRepository repository)
    {
        _repository = repository;
    }

    public async void Run()
    {
        var completeTasks = await _repository.ListCompleteTasks();
        
        Console.WriteLine("Current complete tasks:");
        
        foreach (var item in completeTasks.Data)
        {
            Console.WriteLine(item.Name);
            Console.WriteLine(item.IsComplete);
            Console.WriteLine(item.CreatedAt);
            Console.WriteLine(item.Id);
        }
    }
}