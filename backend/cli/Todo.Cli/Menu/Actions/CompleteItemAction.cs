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
        var taskName = Console.ReadLine();
        var task = _repository.ListToDoFromTask(taskName);
        if (task != null) 
        {
            _repository.CompleteTask(task.Id);
        }

    }
}