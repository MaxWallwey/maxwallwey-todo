namespace Todo.Cli;

public class ToDoRepository : BaseRepository
{
    public void Add(string newTask)
    {
        Items.Add(new Todo(newTask));
    }

}