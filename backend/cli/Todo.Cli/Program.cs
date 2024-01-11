using System.Globalization;
using Todo.Cli.Menu;

List<Todo.Cli.Todo> myItems = new List<Todo.Cli.Todo>()
{
    new("hoover", isComplete:false),
    new("mop", isComplete:false),
    new("washing", isComplete:true)
};

var actions = new Dictionary<char, IMenuAction>()
{
    { 'i', new ListIncompleteItemsAction(myItems) },
    { 'c', new ListCompleteItemsAction(myItems) },
    { 'a', new AddNewItemAction(myItems) },
    { 'm', new CompleteItemAction(myItems) },
    { 'r', new RemoveItemAction(myItems) },
    { 'e', new ExitApplicationAction(myItems) }
};

bool running = true;

while (running == true)
{
    Console.WriteLine(
        "What would you like to do?\n- To view all incompleted tasks, press 'i'\n- To view all completed tasks, press'c'\n- To add a new task, press 'a'\n- To mark a task as completed, press 'm'\n- To remove a task, press 'r'\n- To stop the application, press 'e'\n");
    
    char userInput = Console.ReadKey().KeyChar;
    
    Console.Clear();

    if (actions.TryGetValue(userInput, out var action))
    {
        action.Run();
    }

    //view all tasks
    // if (userInput == 'v')
    // {
    //     Console.WriteLine("All tasks:");
    //
    //     foreach (var item in myItems)
    //     {
    //         Console.WriteLine(item.Name);
    //     }
    // }

    //view incompleted tasks
    // if (userInput == 'i')
    // {
    //     Console.WriteLine("Current incompleted tasks:");
    //
    //     foreach (var item in myItems)
    //     {
    //         if (item.IsComplete == false)
    //         {
    //             Console.WriteLine(item.Name);
    //         }
    //     }
    // }

    //view completed tasks
    // if (userInput == 'c')
    // {
    //     Console.WriteLine("Current completed tasks:");
    //
    //     foreach (var item in myItems)
    //     {
    //         if (item.IsComplete == true)
    //         {
    //             Console.WriteLine(item.Name);
    //         }
    //     }
    // }

    //add new task
    // if (userInput == 'a')
    // {
    //     Console.WriteLine("What task would you like to add? To cancel this, type 'exit'\n");
    //     string? newTask = Console.ReadLine();
    //
    //     if (newTask != null && newTask != "exit")
    //     {
    //         myItems.Add(new Todo.Cli.Todo(newTask));
    //     }
    //
    //     Console.Clear();
    //
    //     Console.WriteLine("All tasks:");
    //
    //     foreach (var item in myItems)
    //     {
    //         Console.WriteLine(item.Name);
    //     }
    // }

    //complete a task
    // if (userInput == 'm')
    // {
    //     Console.WriteLine("What task would you like to mark as completed? To cancel this, type 'exit'\n");
    //     var completeTask = Console.ReadLine();
    //
    //     if (completeTask != "exit")
    //     {
    //         var taskToComplete = myItems.FirstOrDefault(i => i.Name == completeTask);
    //
    //         if (taskToComplete?.IsComplete != null || false)
    //         {
    //             taskToComplete.Complete();
    //         }
    //     }
    //
    //     Console.Clear();
    //
    //     Console.WriteLine("Current completed tasks:");
    //
    //     foreach (var item in myItems)
    //     {
    //         if (item.IsComplete == true)
    //         {
    //             Console.WriteLine(item.Name);
    //         }
    //     }
    // }

    //remove task
    // if (userInput == 'r')
    // {
    //     Console.WriteLine("What task would you like to remove? To cancel this, type 'exit'\n");
    //     var removeTask = Console.ReadLine();
    //
    //     var itemToRemove = myItems.FirstOrDefault(i => i.Name == removeTask);
    //
    //     if (itemToRemove != null && removeTask != "exit")
    //     {
    //         myItems.Remove(itemToRemove);
    //     }
    //
    //     Console.Clear();
    //
    //     Console.WriteLine("All tasks:");
    //
    //     foreach (var item in myItems)
    //     {
    //         Console.WriteLine(item.Name);
    //     }
    // }
}