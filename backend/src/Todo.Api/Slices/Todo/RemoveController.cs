using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Todo.Api.Slices.Todo.RemoveToDo;

namespace Todo.Api.Slices.Todo;

[ApiController]
public class RemoveController : BaseController
{    
    // Remove todo
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost("todo.remove")]
    public async Task RemoveToDo(
        [FromQuery]Request request, 
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        await mediator.Send(request, cancellationToken);
    }
}
