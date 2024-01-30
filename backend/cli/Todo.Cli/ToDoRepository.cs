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
    public void RemoveTask(string removeTask)
    {
        var itemToRemove = Items.FirstOrDefault(i => i.Name == removeTask);

        if (itemToRemove != null) Items.Remove(itemToRemove);
    }

    // List incomplete tasks
    public List<ToDo> ListIncompleteTasks()
    {
        return Items.Where(item => !item.IsComplete).ToList();
    }

    // List complete tasks
    public List<ToDo> ListCompleteTasks()
    {
        return Items.Where(item => item.IsComplete).ToList();
    }

    //Complete task
    public void CompleteTask(string completeTask)
    {
        var taskToComplete = Items.FirstOrDefault(i => i.Name == completeTask);
        taskToComplete?.Complete();
    }
}