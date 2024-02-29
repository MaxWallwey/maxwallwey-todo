using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Slices;

namespace Todo.Api.Controllers;

[ApiController]
public class CompleteController : BaseController
{
    // Complete todo
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpPost("todo.complete")]
    public async Task<ActionResult> CompleteToDo(Guid id)
    {
        await Mediator.Send(new CompleteToDo.CompleteToDoRequest(id));
        return NoContent();
    }
    public CompleteController(IMediator mediator) : base(mediator)
    {
    }
}