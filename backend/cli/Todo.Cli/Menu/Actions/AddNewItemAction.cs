namespace Todo.Cli.Menu.Actions;

public class AddNewItemAction : IMenuAction
{
    private readonly ToDoRepository _repository;

    public AddNewItemAction(ToDoRepository repository)
    {
        _repository = repository;
    }
    
    public void Run()
    {
        Console.WriteLine("What task would you like to add? To cancel this, type 'exit'\n");
        string? newTask = Console.ReadLine();

        if (newTask != null && newTask != "exit")
        {
            _repository.AddTask(newTask);
        }
    }
}