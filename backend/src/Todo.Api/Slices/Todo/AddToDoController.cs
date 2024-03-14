using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Todo.Api.Slices.Todo;

[ApiController]
public class AddToDoController : BaseController
{
    [ProducesResponseType(typeof(AddToDo.Response), StatusCodes.Status200OK)]
    [HttpPost("todo.add")]
    public async Task<AddToDo.Response> AddToDo([FromBody] AddToDo.AddToDoRequest request, [FromServices] IMediator mediator)
    {
        var id = await mediator.Send(request);
        return id;
    }
}