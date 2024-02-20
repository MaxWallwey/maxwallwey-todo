namespace Todo.Cli.Menu.Actions;

public class ExitApplicationAction : IMenuAction
{
    public void Run()
    {
        Environment.Exit(0);
    }
}