namespace Todo.Cli.Menu.Actions;

public class RemoveItemAction : IMenuAction
{
    private readonly ToDoRepository _repository;

    public RemoveItemAction(ToDoRepository repository)
    {
        _repository = repository;
    }

    public void Run()
    {
        Console.WriteLine("What task would you like to remove? To cancel this, type 'exit'\n");
        var removeTask = Console.ReadLine();

        _repository.RemoveTask(removeTask);
    }
}