using Microsoft.AspNetCore.Mvc;
using Todo.Api.Domain;
using Todo.Api.Models;

namespace Todo.Api.Controllers;

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
        var todos = await _toDoRepository.FindManyAsync(isComplete);
        
        return new ResponseData<List<ToDo>>(todos);
    }
    
    // List todo using ID
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ToDo))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpGet("todo.findOne")]
    public async Task<ActionResult<ResponseData<ToDo>>> FindOne(Guid id)
    {
        var todo = await _toDoRepository.FindToDoAsync(id);

        if (todo == null)
        {
            return ValidationProblem();
        }
        
        return new ResponseData<ToDo>(todo);
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
    public async Task<ResponseData<Guid>> AddToDo(CreateToDo model)
    {
        var toDoId = await _toDoRepository.AddToDoAsync(model);

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
