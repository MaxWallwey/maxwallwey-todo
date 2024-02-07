using Todo.Cli.Menu.Actions;

namespace Todo.Cli.Menu;

public class MenuNavigation
{
    private readonly List<Option> _options;

    public MenuNavigation(List<Option> options)
    {
        _options = options;
    }

    // Set the default index of the selected item to be the first
    private static int _index;
    
    public void Start()
    {
        WriteMenu(_options, _options[_index]);

        // Store key info in here
        ConsoleKeyInfo keyinfo;
        do
        {
            keyinfo = Console.ReadKey();

            // Handle each key input (down arrow will write the menu again with a different selected item)
            if (keyinfo.Key == ConsoleKey.DownArrow)
            {
                if (_index + 1 < _options.Count)
                {
                    _index++;
                    WriteMenu(_options, _options[_index]);
                }
            }

            if (keyinfo.Key == ConsoleKey.UpArrow)
            {
                if (_index - 1 >= 0)
                {
                    _index--;
                    WriteMenu(_options, _options[_index]);
                }
            }

            // Handle different action for the option
            if (keyinfo.Key == ConsoleKey.Enter)
            {
                _options[_index].Selected.Invoke();
                _index = 0;
            }
        } while (keyinfo.Key != ConsoleKey.X);

        Console.ReadKey();
    }

    private static void WriteMenu(List<Option>? options, Option selectedOption)
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