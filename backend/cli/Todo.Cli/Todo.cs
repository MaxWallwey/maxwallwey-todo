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

    public Guid Id { get; set; }
    public string Name { get; }
    public DateTime CreatedAt { get; set; }
    public bool IsComplete { get; set; }

    public void Complete()
    {
        IsComplete = true;
    }
}