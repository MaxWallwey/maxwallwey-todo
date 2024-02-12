using Refit;
using ToDo.API.SDK;

namespace Todo.Cli;

public class ToDoRepository
{
    private readonly IToDoClient _toDoClient = RestService.For<IToDoClient>("https://localhost:9000");

    // Add task
    public async Task<Guid> AddTask(string newTodo)
    {
        var model = new CreateToDo
        {
            Name = newTodo
        };

        var response = await _toDoClient.AddToDo(model);

        return response.Data;
    }

    // Remove task
    public void RemoveTask(Guid removeTask)
    {
        _toDoClient.RemoveToDo(removeTask);
    }

    // List incomplete tasks
    public async Task<ResponseData<List<ToDo.API.SDK.ToDo>>> ListIncompleteTasks()
    {
        var todos = await _toDoClient.FindManyFalse();

        return todos;
    }

    // List complete tasks
    public async Task<ResponseData<List<ToDo.API.SDK.ToDo>>> ListCompleteTasks()
    {
        var todos = await _toDoClient.FindManyTrue();

        return todos;
    }
    
    //List ToDo for task
    public async Task<ToDo.API.SDK.ToDo> ListToDoFromTask(Guid id)
    {
        var todo = await _toDoClient.FindOne(id);

        return todo;
    }

    //Complete task
    public void CompleteTask(Guid id)
    {
        _toDoClient.CompleteToDo(id);
    }
}