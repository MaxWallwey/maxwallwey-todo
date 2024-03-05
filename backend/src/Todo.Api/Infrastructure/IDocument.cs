using MongoDB.Bson;

namespace Todo.Api.Infrastructure;

public interface IDocument
{
    public DateTime CreatedAt { get; set; }
    public ObjectId Id { get; set; }
}