using Todo.Api.Domain.Todo;

namespace Todo.Api.Infrastructure;

public interface IDocumentRepository
{
    public Task<List<ToDoDocument>?> FindManyAsync(bool? isComplete);

    public Task<ToDoDocument?> FindOneToDoAsync(Guid id);

    public Task<bool> AnyAsync(string name);

    public Task CompleteToDoAsync(Guid id);

    public Task<Guid> AddToDoAsync(string name);

    public Task RemoveToDoAsync(Guid id);
}