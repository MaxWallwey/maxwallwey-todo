namespace Todo.Cli;

public class ToDoRepository
{
    private readonly List<Todo> _items;

    public ToDoRepository(List<Todo> items)
    {
        _items = items;
    }
    
    public ToDoRepository()
    {
        _items = new List<Todo>();
    }
    
    // Add task
    public void AddTask(string? newTask)
    {
        _items.Add(new Todo(newTask));
    }

    // Remove task
    public void RemoveTask(string? removeTask)
    {
        var itemToRemove = _items.FirstOrDefault(i => i.Name == removeTask);

        if (itemToRemove != null) _items.Remove(itemToRemove);
    }

    // List incomplete tasks
    public List<Todo> ListIncompleteTasks()
    {
        return _items.Where(item => !item.IsComplete).ToList();
    }

    // List complete tasks
    public List<Todo> ListCompleteTasks()
    {
        return _items.Where(item => item.IsComplete).ToList();
    }

    //Complete task
    public void CompleteTask(string? completeTask)
    {
        var taskToComplete = _items.FirstOrDefault(i => i.Name == completeTask);
        taskToComplete?.Complete();
    }
}