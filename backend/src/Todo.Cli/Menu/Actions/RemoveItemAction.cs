using Refit;
using ToDo.Api.Sdk;

namespace Todo.Cli.Menu.Actions;

public class RemoveItemAction : IMenuAction
{
    public RemoveItemAction(IToDoClient toDoClient)
    {
        ToDoClient = toDoClient;
    }
    private IToDoClient ToDoClient { get; }
    
    public async Task Run()
    {
        Console.WriteLine("What task would you like to remove?\n");
        var removeTask = Guid.Parse(Console.ReadLine());

        await ToDoClient.RemoveToDo(removeTask);
    }
}