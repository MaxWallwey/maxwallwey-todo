namespace Todo.Cli;

public class Todo
{
    public Todo(string name, bool isComplete = false)
    {
        Id = Guid.NewGuid();
        Name = name;
        CreatedAt = DateTime.Now;
        IsComplete = isComplete;
    }

    public Guid Id { get; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; }
    public bool IsComplete { get; set; }

    public void Complete()
    {
        IsComplete = true;
    }
}