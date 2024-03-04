namespace Todo.Api.Domain;

public interface IToDoRepository
{
    public Task<bool> AnyAsync(string name);
    public Task<List<ToDo>?> FindManyAsync(bool? isComplete);

    public Task<ToDo?> FindOneToDoAsync(Guid id);

    public Task CompleteToDoAsync(Guid id);

    public Task<Guid> AddToDoAsync(string name);

    public Task RemoveToDoAsync(Guid id);
}