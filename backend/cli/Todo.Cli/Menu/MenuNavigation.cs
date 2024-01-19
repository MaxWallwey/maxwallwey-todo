using Todo.Cli.Menu.Actions;

namespace Todo.Cli.Menu;

public class MenuNavigation : ToDoRepository
{
        public static List<Option>? Options;
    
        // Set the default index of the selected item to be the first
        public static int Index;

        public void Start()
        {
            // Create options that you want your menu to have
            Options = new List<Option>
            {
                new Option("View all incompleted tasks", () => new ListIncompleteItemsAction(Items).Run()),
                new Option("View all completed tasks", () => new ListCompleteItemsAction(Items).Run()),
                new Option("Add a new task", () => new AddNewItemAction(Items).Run()),
                new Option("Mark task as completed", () => new CompleteItemAction(Items).Run() ),
                new Option("Remove a task", () => new RemoveItemAction(Items).Run() ),
                new Option("Exit", () => Environment.Exit(0)),
            };
            
            WriteMenu(Options, Options[Index]);

            // Store key info in here
            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();

                // Handle each key input (down arrow will write the menu again with a different selected item)
                if (keyinfo.Key == ConsoleKey.DownArrow)
                {
                    if (Index + 1 < Options.Count)
                    {
                        Index++;
                        WriteMenu(Options, Options[Index]);
                    }
                }
                if (keyinfo.Key == ConsoleKey.UpArrow)
                {
                    if (Index - 1 >= 0)
                    {
                        Index--;
                        WriteMenu(Options, Options[Index]);
                    }
                }
                // Handle different action for the option
                if (keyinfo.Key == ConsoleKey.Enter)
                {
                    Options[Index].Selected.Invoke();
                    Index = 0;
                }
            }
            while (keyinfo.Key != ConsoleKey.X);

            Console.ReadKey();

        }
        // 
        
        public static void WriteMenu(List<Option>? options, Option selectedOption)
        {
            Console.Clear();

            if (options != null)
                foreach (Option option in options)
                {
                    if (option == selectedOption)
                    {
                        Console.Write("> ");
                    }
                    else
                    {
                        Console.Write(" ");
                    }

                    Console.WriteLine(option.Name);
                }
        }
}

    public class Option
    {
        public string Name { get; }
        public Action Selected { get; }

        public Option(string name, Action selected)
        {
            Name = name;
            Selected = selected;
        }
    }