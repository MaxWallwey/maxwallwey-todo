using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.API.Models;

namespace Todo.API.Controllers;

[ApiController]
public class ToDoController : ControllerBase
{
    private readonly ToDoContext _context;

    public ToDoController(ToDoContext context)
    {
        _context = context;
    }

    //List all todos with optional complete parameter
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ToDo))]
    [HttpGet ("todo.findMany")]
    public async Task<List<ToDo?>> FindMany(bool? isComplete)
    {
        if (isComplete == null)
        {
            return await _context.Todos.ToListAsync();
        }
        return await _context.Todos.Where(i => i.IsComplete == isComplete).ToListAsync();
    }

    //Complete todo
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpPost("complete")]
    public async Task<IActionResult> CompleteToDo(Guid id)
    {
        var todo = await _context.Todos.FindAsync(id);

        if (todo is null)
        {
            return ValidationProblem("No matching todo was found");
        }
        
        todo?.Complete();
        
        return Ok();
    }

    //Add new todo
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpPost ("add")]
    public async Task<ActionResult<Guid>> AddToDo(CreateToDo toDo)
    {
        if (toDo.Name is null)
        {
            var problemDetails = new ValidationProblemDetails();
            return new ObjectResult(problemDetails)
            {
                ContentTypes = { "application/problem+json" },
                StatusCode = 400
            };
        }
        
        _context.Todos.Add(new ToDo(toDo.Name!));

        await _context.SaveChangesAsync();

        return Ok(_context.Todos.FirstOrDefault(i => i.Name == toDo.Name).Id);
    }

    //Remove todo
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpDelete("remove")]
    public async Task<IActionResult> RemoveToDo(Guid id)
    {
        var todo = await _context.Todos.FindAsync(id);
        
        if (todo is null)
        {
            return ValidationProblem("No matching todo was found");
        }

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();

        return Ok();
    }
}