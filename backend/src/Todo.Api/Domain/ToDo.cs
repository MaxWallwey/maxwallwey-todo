using System;
using System.ComponentModel.DataAnnotations;

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
    
    protected internal ToDo(){}

    public Guid Id { get; set; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; set; }
    public bool IsComplete { get; private set; }

    public void Complete()
    {
        IsComplete = true;
    }
}