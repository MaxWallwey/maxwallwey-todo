using MongoDB.Bson;
using Todo.Api.Domain.Todo;
using Todo.Api.Infrastructure;

namespace Todo.Api.Domain.Infrastructure;

public interface IDocumentRepository<TDocument>
    where TDocument : IDocument
{
    public Task<List<ToDoDocument>?> FindManyAsync(bool? isComplete);

    public Task<ToDoDocument?> FindOneToDoAsync(ObjectId id);

    public Task<bool> AnyAsync(string name);

    public Task UpdateToDoAsync(ToDoDocument document);

    public Task<ObjectId> AddToDoAsync(string name);

    public Task RemoveToDoAsync(ObjectId id);
}