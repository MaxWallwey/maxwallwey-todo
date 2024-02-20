using Refit;
using ToDo.API.SDK;

namespace Todo.Cli.Menu.Actions;

public class ListIncompleteItemsAction : IMenuAction
{
    private readonly IToDoClient _toDoClient = RestService.For<IToDoClient>("https://localhost:9000");

    public async void Run()
    {
        var todos = await _toDoClient.FindMany(false);
        
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