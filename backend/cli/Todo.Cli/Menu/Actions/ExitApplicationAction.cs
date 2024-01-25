namespace Todo.Cli.Menu.Actions;

public class ExitApplicationAction : IMenuAction
{
    public void Run()
    {
        System.Environment.Exit(0);
    }
}