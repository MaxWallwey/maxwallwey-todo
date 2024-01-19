namespace Todo.Cli;

public class BaseRepository
{
    public static List<Todo> Items = new List<Todo>{new Todo("Buy some milk"), new Todo("Call the dentist"), new Todo("Cancel Netflix", true)};
}