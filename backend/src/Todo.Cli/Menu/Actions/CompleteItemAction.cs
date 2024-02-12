namespace Todo.Cli.Menu.Actions;

public class CompleteItemAction : IMenuAction
{
    private readonly ToDoRepository _repository;

    public CompleteItemAction(ToDoRepository repository)
    {
        _repository = repository;
    }

    public void Run()
    {
        var id = Guid.Parse(Console.ReadLine());
        _repository.CompleteTask(id);
    }
}