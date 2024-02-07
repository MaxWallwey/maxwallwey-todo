using Todo.API.Models;

namespace Todo.API.Domain;

public interface IToDoRepository
{
    public Task<List<ToDo>> FindManyAsync(bool? isComplete);

    public Task<ToDo?> FindToDoAsync(Guid id);

    public Task CompleteToDoAsync(Guid id);

    public Task<Guid> AddToDoAsync(CreateToDo toDo);

    public Task RemoveToDoAsync(Guid id);
}