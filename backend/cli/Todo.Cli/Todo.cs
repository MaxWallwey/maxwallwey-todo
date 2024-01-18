using System.Runtime.Serialization;

namespace Todo.Cli;

public class Todo
{
    public Todo(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        CreatedAt = DateTime.Now;
        IsComplete = false;
    }

    public Guid Id { get; }
    public string Name { get; }
    public DateTime CreatedAt { get; }
    public bool IsComplete { get; private set; }

    public void Complete()
    {
        IsComplete = true;
    }
}