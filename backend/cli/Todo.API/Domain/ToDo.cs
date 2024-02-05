using System.ComponentModel.DataAnnotations;

namespace Todo.API.Models;

public class ToDo
{
    protected internal ToDo(string? name, bool isComplete = false)
    {
        Id = Guid.NewGuid();
        Name = name;
        CreatedAt = DateTime.UtcNow;
        IsComplete = isComplete;
    }

    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Name should be minimum 3 characters and a maximum of 50 characters")]
    [DataType(DataType.Text)]
    public string Name { get; private set; }
    public DateTime CreatedAt { get; set; }
    public bool IsComplete { get; private set; }

    public void Complete()
    {
        IsComplete = true;
    }
}