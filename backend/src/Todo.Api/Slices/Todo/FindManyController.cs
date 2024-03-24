using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Domain.Todo;
using static Todo.Api.Slices.Todo.FindManyToDo;

namespace Todo.Api.Slices.Todo;

[ApiController]
public class FindManyController : BaseController
{
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ToDoDocument>))]
    [HttpGet("todo.findMany")]
    public async Task<Response> FindMany(
        [FromQuery]bool? isComplete, 
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new Request(isComplete), cancellationToken);
    }
}