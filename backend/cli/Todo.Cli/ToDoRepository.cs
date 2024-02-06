namespace Todo.Cli;

public class ToDoRepository
{
    public List<ToDo> Items { get; }

    public ToDoRepository(List<ToDo> items)
    {
        Items = items;
    }
    
    public ToDoRepository()
    {
        Items = new List<ToDo>();
    }
    
    // Add task
    public void AddTask(string newTask)
    {
        Items.Add(new ToDo(newTask));
    }

    // Remove task
    public void RemoveTask(Guid removeTask)
    {
        var itemToRemove = Items.FirstOrDefault(i => i.Id == removeTask);

        if (itemToRemove != null) Items.Remove(itemToRemove);
    }

    // List incomplete tasks
    public List<ToDo> ListIncompleteTasks()
    {
        return Items.Where(i => !i.IsComplete).ToList();
    }

    // List complete tasks
    public List<ToDo> ListCompleteTasks()
    {
        return Items.Where(i => i.IsComplete).ToList();
    }
    
    //List ToDo for task
    public ToDo? ListToDoFromTask(string? name)
    {
        return Items.FirstOrDefault(i => i.Name == name);
    }

    //Complete task
    public void CompleteTask(Guid id)
    {
        var taskToComplete = Items.FirstOrDefault(i => i.Id == id);
        taskToComplete?.Complete();
    }
}