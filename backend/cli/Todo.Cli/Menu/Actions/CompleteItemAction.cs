namespace Todo.Cli.Menu.Actions;

public class CompleteItemAction : IMenuAction
{
    private readonly List<Todo> _items;

    public CompleteItemAction(List<Todo> items)
    {
        _items = items;
    }

    public void Run()
    {
        Console.WriteLine("What task would you like to mark as completed? To cancel this, type 'exit'\n");
        var completeTask = Console.ReadLine();

        if (completeTask != "exit")
        {
            var taskToComplete = _items.FirstOrDefault(i => i.Name == completeTask);

            if (taskToComplete?.IsComplete != null || false)
            {
                taskToComplete.Complete();
            }
        }
    }
}