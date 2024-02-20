using Refit;
using ToDo.Api.Sdk;

namespace Todo.Cli.Menu.Actions;

public class RemoveItemAction : IMenuAction
{
    private readonly IToDoClient _toDoClient = RestService.For<IToDoClient>("https://localhost:9000");
    
    public void Run()
    {
        Console.WriteLine("What task would you like to remove?\n");
        var removeTask = Guid.Parse(Console.ReadLine());

        _toDoClient.RemoveToDo(removeTask);
    }
}