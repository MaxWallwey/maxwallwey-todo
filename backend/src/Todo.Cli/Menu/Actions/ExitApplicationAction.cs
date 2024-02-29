namespace Todo.Cli.Menu.Actions;

public class ExitApplicationAction : IMenuAction
{
    public Task Run()
    {
        Environment.Exit(0);
        return Task.CompletedTask;
    }
}