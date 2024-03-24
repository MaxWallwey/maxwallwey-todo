using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Todo.Api.Domain.Todo;
using static Todo.Api.Slices.Todo.FindOneToDo;

namespace Todo.Api.Slices.Todo;

[ApiController]
public class FindOneController : BaseController
{
    // List ToDo based on ID
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ToDoDocument))]
    [HttpGet("todo.findOne")]
    public async Task<Response> FindOne(
        [FromQuery]ObjectId id, 
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    { 
        return await mediator.Send(new Request(id), cancellationToken);
    }
}