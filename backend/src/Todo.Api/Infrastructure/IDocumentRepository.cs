using MongoDB.Bson;
using Todo.Api.Domain.Todo;

namespace Todo.Api.Infrastructure;

public interface IDocumentRepository
{
    public Task<List<ToDoDocument>?> FindManyAsync(bool? isComplete);

    public Task<ToDoDocument?> FindOneToDoAsync(string id);

    public Task<bool> AnyAsync(string name);

    public Task CompleteToDoAsync(string id);

    public Task<string> AddToDoAsync(string name);

    public Task RemoveToDoAsync(string id);
}