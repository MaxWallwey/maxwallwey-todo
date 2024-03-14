using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Todo.Api.Slices.Todo;

[ApiController]
public class CompleteController : BaseController
{
    // Complete todo
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost("todo.complete")]
    public async Task CompleteToDo([FromQuery]CompleteToDo.CompleteToDoRequest request, [FromServices] IMediator mediator)
    {
        await mediator.Send(request);
    }
}