using Microsoft.AspNetCore.Mvc;
using Todo.Cli;

namespace Todo.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDoController : ControllerBase
{
    private readonly ToDoRepository _repository;

    public ToDoController(ToDoRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet ("{complete tasks}")]
    public Task<ActionResult<List<ToDo>>> GetCompleteTasks()
    {
        return Task.FromResult<ActionResult<List<ToDo>>>(_repository.ListCompleteTasks());
    }

    [HttpGet ("{incomplete tasks}")]
    public Task<ActionResult<List<ToDo>>> GetIncompleteTasks()
    {
        return Task.FromResult<ActionResult<List<ToDo>>>(_repository.ListIncompleteTasks());
    }

    [HttpPost]
    public Task<Guid> CreateNewTask(string name)
    {
        _repository.AddTask(name);
        return Task.FromResult(_repository.ListToDoFromTask(name)!.Id);
    }

    [HttpPost]
    public Task<ActionResult> CompleteTask(Guid id)
    {
        _repository.CompleteTaskUsingId(id);
        return Task.FromResult<ActionResult>(Ok());
    }

    [HttpPost]
    public Task<ActionResult> RemoveTask(Guid id)
    {
        _repository.RemoveTaskUsingId(id);
        return Task.FromResult<ActionResult>(Ok());
    }
}