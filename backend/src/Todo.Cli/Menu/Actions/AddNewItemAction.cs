using Refit;
using ToDo.Api.Sdk;

namespace Todo.Cli.Menu.Actions;

public class AddNewItemAction : IMenuAction
{
    public AddNewItemAction(IToDoClient toDoClient)
    {
        ToDoClient = toDoClient;
    }
    private IToDoClient ToDoClient { get; }

    public async Task Run()
    {
        Console.WriteLine("What task would you like to add? To cancel this, type 'exit'\n");
        string? newTask = Console.ReadLine();

        if (newTask == "exit")
        {
            return;
        }

        var model = new CreateToDo { Name = newTask };

        var response = await ToDoClient.AddToDo(model);
        
        Console.WriteLine($"ID: {response.Data}");
    }
}