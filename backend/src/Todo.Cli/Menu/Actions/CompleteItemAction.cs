using Refit;
using Todo.Api.Sdk;

namespace Todo.Cli.Menu.Actions;

public class CompleteItemAction : IMenuAction
{
    public CompleteItemAction(IToDoClient toDoClient)
    {
        ToDoClient = toDoClient;
    }
    private IToDoClient ToDoClient { get; }
    public async Task Run()
    {
        var id = Guid.Parse(Console.ReadLine());
        await ToDoClient.CompleteToDo(id);
    }
}