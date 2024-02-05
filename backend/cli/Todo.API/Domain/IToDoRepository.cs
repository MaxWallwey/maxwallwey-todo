using Todo.API.Models;

namespace Todo.API.Domain;

public interface IToDoRepository
{
    public Task<List<ToDo>> FindMany(bool? isComplete);

    public Task<ToDo?> FindToDo(Guid id);

    public Task CompleteToDo(Guid id);

    public Task<Guid> AddToDo(CreateToDo toDo);

    public Task RemoveToDo(Guid id);
}