using Microsoft.AspNetCore.Mvc;
using Todo.Api.Domain;
using Todo.Api.Models;

namespace Todo.Api.Slices;

[ApiController]
public class ToDoController : BaseController
{
    // List all todos with optional complete parameter
    public ToDoController(IToDoRepository toDoRepository) : base(toDoRepository)
    {
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ToDo))]
    [HttpGet("todo.findMany")]
    public async Task<ResponseData<List<ToDo>>> FindMany(bool? isComplete)
    {
        var todos = await ToDoRepository.FindManyAsync(isComplete);
        
        return new ResponseData<List<ToDo>>(todos);
    }
    
    // List todo using ID
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ToDo))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpGet("todo.findOne")]
    public async Task<ActionResult<ResponseData<ToDo>>> FindOne(Guid id)
    {
        var todo = await ToDoRepository.FindToDoAsync(id);

        if (todo == null)
        {
            return ValidationProblem();
        }
        
        return new ResponseData<ToDo>(todo);
    }

    // Complete todo
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpPost("todo.complete")]
    public async Task<ActionResult<ResponseData<ToDo>>> CompleteToDo(Guid id)
    {
        var todo = await ToDoRepository.FindToDoAsync(id);
        
        if (todo == null)
        {
            return ValidationProblem("No matching todo was found");
        }
        
        await ToDoRepository.CompleteToDoAsync(id);

        return NoContent();
    }

    // Add new todo
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpPost("todo.add")]
    public async Task<ResponseData<Guid>> AddToDo(CreateToDo model)
    {
        var toDoId = await ToDoRepository.AddToDoAsync(model);

        return new ResponseData<Guid>(toDoId);
    }

    // Remove todo
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpDelete("todo.remove")]
    public async Task<IActionResult> RemoveToDo(Guid id)
    {
        var todo = await ToDoRepository.FindToDoAsync(id);
        
        if (todo == null)
        {
            return ValidationProblem("No matching todo was found");
        }

        await ToDoRepository.RemoveToDoAsync(id);

        return NoContent();
    }
}
