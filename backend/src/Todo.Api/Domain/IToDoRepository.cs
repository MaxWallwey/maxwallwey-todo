using Todo.Api.Models;

namespace Todo.Api.Domain;

public interface IToDoRepository
{
    public Task<ResponseData<List<ToDo>>> FindManyAsync(bool? isComplete);

    public Task<ResponseData<ToDo>?> FindOneToDoAsync(Guid id);

    public Task CompleteToDoAsync(Guid id);

    public Task<ResponseData<Guid>> AddToDoAsync(CreateToDo toDo);

    public Task RemoveToDoAsync(Guid id);
}