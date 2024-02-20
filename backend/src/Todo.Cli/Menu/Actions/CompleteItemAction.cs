using Refit;
using ToDo.API.SDK;

namespace Todo.Cli.Menu.Actions;

public class CompleteItemAction : IMenuAction
{
    private readonly IToDoClient _toDoClient = RestService.For<IToDoClient>("https://localhost:9000");
    
    public async void Run()
    {
        var id = Guid.Parse(Console.ReadLine());
        await _toDoClient.CompleteToDo(id);
    }
}