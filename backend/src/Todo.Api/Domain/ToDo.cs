namespace Todo.Api.Domain;

public class ToDo
{
    public ToDo(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        CreatedAt = DateTime.UtcNow;
        IsComplete = false;
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsComplete { get; set; }

    public void Complete()
    {
        IsComplete = true;
    }
}