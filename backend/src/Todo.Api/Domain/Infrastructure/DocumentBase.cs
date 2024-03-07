using MongoDB.Bson;

namespace Todo.Api.Domain.Infrastructure;

public abstract class DocumentBase : IDocument
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
}