using System.ComponentModel.DataAnnotations;
using Todo.Api.Domain.Infrastructure;

namespace Todo.Api.Domain.Todo;

public class ToDoDocument : DocumentBase
{
    public ToDoDocument(string userId, string name)
    {
        Name = name;
        IsComplete = false;
        UserId = userId;
    }
    
    public string Name { get; set; }
    public bool IsComplete { get; private set; }
    public string UserId { get; private set; }

    public void Complete()
    {
        IsComplete = true;
    }
}