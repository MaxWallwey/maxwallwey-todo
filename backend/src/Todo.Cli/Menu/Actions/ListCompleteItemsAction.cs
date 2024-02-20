using Refit;
using ToDo.Api.Sdk;

namespace Todo.Cli.Menu.Actions;

public class ListCompleteItemsAction : IMenuAction
{
    private readonly IToDoClient _toDoClient = RestService.For<IToDoClient>("https://localhost:9000");

    public async void Run()
    {
        var todos = await _toDoClient.FindMany(true);
        
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