using Todo.Api.Domain.Models;

namespace Todo.Api.Domain;

public interface IToDoRepository
{
    public Task<bool> AnyAsync(string name);
    public Task<ResponseData<List<ToDo>>?> FindManyAsync(bool? isComplete);

    public Task<ResponseData<ToDo>?> FindOneToDoAsync(Guid id);

    public Task CompleteToDoAsync(Guid id);

    public Task<ResponseData<Guid>> AddToDoAsync(string name);

    public Task RemoveToDoAsync(Guid id);
}