namespace Todo.Cli;

public class ToDoRepository : BaseRepository
{
    // Add task
    public void AddTask(string? newTask)
    {
        if (newTask != null && newTask != "exit")
        {
            Items.Add(new Todo(newTask));
        }
    }

    // Remove task
    public void RemoveTask(string? removeTask)
    {
        var itemToRemove = Items.FirstOrDefault(i => i.Name == removeTask);

        if (itemToRemove != null && removeTask != "exit")
        {
            Items.Remove(itemToRemove);
        }
    }

    // List incomplete tasks
    public void ListIncompleteTasks()
    {
        foreach (var item in Items.Where(item => item.IsComplete == false))
        {
            Console.WriteLine(item.Name);
        }
    }

    // List complete tasks
    public void ListCompleteTasks()
    {
        foreach (var item in Items.Where(item => item.IsComplete))
        {
            Console.WriteLine(item.Name);
        }
    }

    //Complete task
    public void CompleteTask(string? completeTask)
    {
        if (completeTask != "exit")
        {
            var taskToComplete = Items.FirstOrDefault(i => i.Name == completeTask);

            if (taskToComplete?.IsComplete != null || false)
            {
                taskToComplete.IsComplete = true;
            }
        }
    }
}