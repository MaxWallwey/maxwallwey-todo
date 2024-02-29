using System.ComponentModel.DataAnnotations;
using Todo.Api.Infrastructure;

namespace Todo.Api.Domain.Todo;

public class ToDoDocument : DocumentBase
{
    public ToDoDocument(string name)
    {
        Name = name;
        IsComplete = false;
    }
    
    [MaxLength(100)]
    public string Name { get; set; }
    public bool IsComplete { get; private set; }

    public void Complete()
    {
        IsComplete = true;
    }
}