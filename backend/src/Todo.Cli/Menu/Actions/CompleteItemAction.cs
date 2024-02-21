using Refit;
using ToDo.Api.Sdk;

namespace Todo.Cli.Menu.Actions;

public class CompleteItemAction : IMenuAction
{
    private readonly IToDoClient _toDoClient = RestService.For<IToDoClient>("https://localhost:9000");
    
    public async Task Run()
    {
        var id = Guid.Parse(Console.ReadLine());
        await _toDoClient.CompleteToDo(id);
    }
}