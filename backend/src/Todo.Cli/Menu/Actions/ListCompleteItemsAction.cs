using Refit;
using Todo.Api.Sdk;

namespace Todo.Cli.Menu.Actions;

public class ListCompleteItemsAction : IMenuAction
{
    public ListCompleteItemsAction(IToDoClient toDoClient)
    {
        ToDoClient = toDoClient;
    }
    private IToDoClient ToDoClient { get; }

    public async Task Run()
    {
        var todos = await ToDoClient.FindMany(true);
        
        Console.WriteLine("Current complete tasks:");
        
        foreach (var item in todos.Data)
        {
            Console.WriteLine(item.Name);
            Console.WriteLine(item.IsComplete);
            Console.WriteLine(item.CreatedAt);
            Console.WriteLine(item.Id);
        }
    }
}