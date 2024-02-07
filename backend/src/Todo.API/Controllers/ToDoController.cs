using Microsoft.AspNetCore.Mvc;
using Todo.API.Domain;
using Todo.API.Models;

namespace Todo.API.Controllers;

[ApiController]
public class ToDoController : ControllerBase
{
    private readonly IToDoRepository _toDoRepository;

    public ToDoController(IToDoRepository toDoRepository)
    {
        _toDoRepository = toDoRepository;
    }
    
    // List all todos with optional complete parameter
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ToDo))]
    [HttpGet("todo.findMany")]
    public async Task<ResponseData<List<ToDo>>> FindMany(bool? isComplete)
    {
        return new ResponseData<List<ToDo>>(await _toDoRepository.FindManyAsync(isComplete));
    }

    // Complete todo
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpPost("todo.complete")]
    public async Task<IActionResult> CompleteToDo(Guid id)
    {
        var todo = await _toDoRepository.FindToDoAsync(id);
        
        if (todo == null)
        {
            return ValidationProblem("No matching todo was found");
        }
        
        await _toDoRepository.CompleteToDoAsync(id);
        
        return NoContent();
    }

    // Add new todo
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpPost("todo.add")]
    public async Task<ResponseData<Guid>> AddToDo(CreateToDo toDo)
    {
        var toDoId = await _toDoRepository.AddToDoAsync(toDo);

        return new ResponseData<Guid>(toDoId);
    }

    // Remove todo
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpDelete("todo.remove")]
    public async Task<IActionResult> RemoveToDo(Guid id)
    {
        var todo = await _toDoRepository.FindToDoAsync(id);
        
        if (todo == null)
        {
            return ValidationProblem("No matching todo was found");
        }

        await _toDoRepository.RemoveToDoAsync(id);

        return NoContent();
    }
}
