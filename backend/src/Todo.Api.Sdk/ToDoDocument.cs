using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace Todo.Api.Sdk;

public class ToDoDocument
{
    public ToDoDocument(string userId, string name)
    {
        Name = name;
        IsComplete = false;
        UserId = userId;
    }

    [MaxLength(100)]
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsComplete { get; private set; }
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
    public string UserId { get; private set; }
    
    public void Complete()
    {
        IsComplete = true;
    }
}