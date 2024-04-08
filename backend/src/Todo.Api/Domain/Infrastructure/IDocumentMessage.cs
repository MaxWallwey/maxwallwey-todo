using MongoDB.Bson;

namespace Todo.Api.Domain.Infrastructure;

public interface IDocumentMessage
{
    ObjectId Id { get; }
}