namespace Todo.Api.Sdk;

public class ToDo
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsComplete { get; set; }
}