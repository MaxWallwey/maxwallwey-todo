using Todo.Api.Domain.Todo;
using Todo.Api.Models;

namespace Todo.Api.Infrastructure;

public interface IDocumentRepository
{
    public Task<ResponseData<List<ToDoDocument>>> FindManyAsync(bool? isComplete);

    public Task<ResponseData<ToDoDocument>?> FindOneToDoAsync(Guid id);

    public Task CompleteToDoAsync(Guid id);

    public Task<ResponseData<Guid>> AddToDoAsync(CreateToDo toDo);

    public Task RemoveToDoAsync(Guid id);
}