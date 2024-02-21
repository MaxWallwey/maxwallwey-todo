namespace Todo.Cli.Menu.Actions;

public class ExitApplicationAction : IMenuAction
{
    public async Task Run()
    {
        Environment.Exit(0);
    }
}