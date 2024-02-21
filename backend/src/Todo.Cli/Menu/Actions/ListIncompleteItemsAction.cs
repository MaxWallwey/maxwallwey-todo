using Refit;
using ToDo.Api.Sdk;

namespace Todo.Cli.Menu.Actions;

public class ListIncompleteItemsAction : IMenuAction
{
    public ListIncompleteItemsAction(IToDoClient toDoClient)
    {
        ToDoClient = toDoClient;
    }
    private IToDoClient ToDoClient { get; }
    
    public async Task Run()
    {
        var todos = await ToDoClient.FindMany(false);
        
        Console.WriteLine("Current incomplete tasks:");
        
        foreach (var item in todos.Data)
        {
            Console.WriteLine(item.Name);
            Console.WriteLine(item.IsComplete);
            Console.WriteLine(item.CreatedAt);
            Console.WriteLine(item.Id);
        }
    }
}