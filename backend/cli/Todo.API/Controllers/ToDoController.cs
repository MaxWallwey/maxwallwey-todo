using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public async Task<List<ToDo>> FindMany(bool? isComplete)
    {
        return await _toDoRepository.FindMany(isComplete);
    }

    // Complete todo
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpPost("todo.complete")]
    public async Task<IActionResult> CompleteToDo(Guid id)
    {
        var todo = await _toDoRepository.FindToDo(id);
        
        if (todo == null)
        {
            return ValidationProblem("No matching todo was found");
        }
        
        await _toDoRepository.CompleteToDo(id);
        
        return Ok();
    }

    // Add new todo
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpPost("todo.add")]
    public async Task<ActionResult<Guid>> AddToDo(CreateToDo toDo)
    {
        var toDoId = await _toDoRepository.AddToDo(toDo);

        return Ok(toDoId);
    }

    // Remove todo
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpDelete("todo.remove")]
    public async Task<IActionResult> RemoveToDo(Guid id)
    {
        var todo = await _toDoRepository.FindToDo(id);
        
        if (todo == null)
        {
            return ValidationProblem("No matching todo was found");
        }

        await _toDoRepository.RemoveToDo(id);

        return Ok();
    }
}