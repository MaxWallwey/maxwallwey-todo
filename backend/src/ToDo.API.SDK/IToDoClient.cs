using Refit;

namespace ToDo.Api.Sdk;

[Headers("Content-Type: application/json")]
public interface IToDoClient
{
    [Get("/todo.findMany")]
    Task<ResponseData<List<ToDo>>> FindMany(bool isComplete);
    
    [Get("/todo.findOne")]
    Task<ToDo> FindOne(Guid id);

    [Post("/todo.complete")]
    Task CompleteToDo(Guid id);

    [Post("/todo.add")]
    Task<ResponseData<Guid>> AddToDo([Body] CreateToDo name);

    [Delete("/todo.remove")]
    Task RemoveToDo(Guid id);
}