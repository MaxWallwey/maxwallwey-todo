using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Domain;
using Todo.Api.Models;

namespace Todo.Api.Slices;

[ApiController]
public class ToDoController : BaseController
{
    // List all todos with optional complete parameter
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseData<List<ToDo>>))]
    [HttpGet("todo.findMany")]
    public async Task<ResponseData<List<ToDo>>> FindMany(bool isComplete)
    {
        return await Mediator.Send(new FindManyToDoRequest(isComplete));
    }
    
    // Complete todo
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpPost("todo.complete")]
    public async Task<ActionResult> CompleteToDo(Guid id)
    {
        await Mediator.Send(new CompleteToDoRequest(id));
        return NoContent();
    }

    // Add new todo
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpPost("todo.add")]
    public async Task<ResponseData<Guid>> AddToDo(CreateToDo model)
    {
        return await Mediator.Send(new AddToDoRequest(model));
    }

    // Remove todo
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpDelete("todo.remove")]
    public async Task<ActionResult> RemoveToDo(Guid id)
    {
        await Mediator.Send(new RemoveToDoRequest(id));
        return NoContent();
    }
    
    public ToDoController(IMediator mediator) : base(mediator)
    {
    }
}
