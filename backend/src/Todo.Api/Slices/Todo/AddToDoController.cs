using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Todo.Api.Slices.Todo.AddToDo;

namespace Todo.Api.Slices.Todo;

[ApiController]
public class AddToDoController : BaseController
{
    [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
    [HttpPost("todo.add")]
    public async Task<Response> AddToDo(
        [FromBody] Request request, 
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var id = await mediator.Send(request, cancellationToken);
        return id;
    }
}