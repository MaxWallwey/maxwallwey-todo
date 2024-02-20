using System;
using System.ComponentModel.DataAnnotations;

namespace Todo.Api.Domain;

public class ToDo
{
    protected internal ToDo(string name, bool isComplete = false)
    {
        Id = Guid.NewGuid();
        Name = name;
        CreatedAt = DateTime.UtcNow;
        IsComplete = isComplete;
    }

    public Guid Id { get; set; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; set; }
    public bool IsComplete { get; private set; }

    public void Complete()
    {
        IsComplete = true;
    }
}