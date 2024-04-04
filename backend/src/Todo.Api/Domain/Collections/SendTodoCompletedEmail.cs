using MongoDB.Bson;
using Todo.Api.Domain.Infrastructure;

namespace Todo.Api.Domain.Collections;

public record SendTodoCompletedEmail(ObjectId ObjectId) : IDocumentMessage
{
    public ObjectId Id { get; private set; } = ObjectId.GenerateNewId();
    public DateTime Timestamp { get; private set; } = DateTime.UtcNow;
}