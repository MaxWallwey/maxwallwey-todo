using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Domain;
using Todo.Api.Models;
using Todo.Api.Slices;

namespace Todo.Api.Controllers;

[ApiController]
public class RemoveController : BaseController
{    
    // Remove todo
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpDelete("todo.remove")]
    public async Task<ActionResult> RemoveToDo(Guid id)
    {
        await Mediator.Send(new RemoveToDo.RemoveToDoRequest(id));
        return NoContent();
    }
    
    public RemoveController(IMediator mediator) : base(mediator)
    {
    }
}
