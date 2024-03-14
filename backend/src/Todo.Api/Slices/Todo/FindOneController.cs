using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Todo.Api.Domain.Todo;

namespace Todo.Api.Slices.Todo;

[ApiController]
public class FindOneController : BaseController
{
    // List ToDo based on ID
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ToDoDocument))]
    [HttpGet("todo.findOne")]
    public async Task<FindOneToDo.Response> FindOne([FromQuery]ObjectId id, [FromServices] IMediator mediator)
    { 
        return await mediator.Send(new FindOneToDo.FindOneToDoRequest(id));
    }
}