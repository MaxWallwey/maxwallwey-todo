using Refit;
using ToDo.Api.Sdk;

namespace Todo.Cli.Menu.Actions;

public class AddNewItemAction : IMenuAction
{
    public AddNewItemAction(IToDoClient toDoClient)
    {
        _toDoClient = toDoClient;
    }
    private IToDoClient _toDoClient { get; }

    public async Task Run()
    {
        Console.WriteLine("What task would you like to add? To cancel this, type 'exit'\n");
        string? newTask = Console.ReadLine();

        if (newTask == "exit")
        {
            return;
        }

        var model = new CreateToDo { Name = newTask };

        var response = await _toDoClient.AddToDo(model);
        
        Console.WriteLine($"ID: {response.Data}");
    }
}