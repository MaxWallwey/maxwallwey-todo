using Todo.Cli.Menu.Actions;

namespace Todo.Cli.Menu;

public class MenuNavigation : ToDoRepository
{
        // public static List<Option>? Options;
    
        // Set the default index of the selected item to be the first
        public static int Index;

        public void Start()
        {
            MenuOptions options = new MenuOptions();
            
            WriteMenu(MenuOptions.Options, MenuOptions.Options[Index]);

            // Store key info in here
            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();

                // Handle each key input (down arrow will write the menu again with a different selected item)
                if (keyinfo.Key == ConsoleKey.DownArrow)
                {
                    if (Index + 1 < MenuOptions.Options.Count)
                    {
                        Index++;
                        WriteMenu(MenuOptions.Options, MenuOptions.Options[Index]);
                    }
                }
                if (keyinfo.Key == ConsoleKey.UpArrow)
                {
                    if (Index - 1 >= 0)
                    {
                        Index--;
                        WriteMenu(MenuOptions.Options, MenuOptions.Options[Index]);
                    }
                }
                // Handle different action for the option
                if (keyinfo.Key == ConsoleKey.Enter)
                {
                    MenuOptions.Options[Index].Selected.Invoke();
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