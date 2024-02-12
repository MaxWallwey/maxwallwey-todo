using Refit;

namespace ToDo.API.SDK;

[Headers("Content-Type: application/json")]
public interface IToDoClient
{
    [Get("/todo.findMany?isComplete=false")]
    Task<ResponseData<List<ToDo>>> FindManyFalse();
    
    [Get("/todo.findMany?isComplete=true")]
    Task<ResponseData<List<ToDo>>> FindManyTrue();
    
    [Get("/todo.findOne")]
    Task<ToDo> FindOne(Guid id);

    [Post("/todo.complete")]
    Task CompleteToDo(Guid id);

    [Post("/todo.add")]
    Task<ResponseData<Guid>> AddToDo(CreateToDo name);

    [Delete("/todo.remove")]
    Task RemoveToDo(Guid id);
}