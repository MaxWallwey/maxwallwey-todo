using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Todo.Api.Slices.Todo.CompleteToDo;

namespace Todo.Api.Slices.Todo;

[ApiController]
public class CompleteController : BaseController
{
    // Complete todo
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost("todo.complete")]
    public async Task CompleteToDo(
        [FromQuery]Request request, 
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        await mediator.Send(request, cancellationToken);
    }
}