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
        Console.WriteLine("What task would you like to mark as completed? To cancel this, type 'exit'\n");
        var completeTask = Console.ReadLine();

        _repository.CompleteTask(Guid.Parse(completeTask!));
    }
}