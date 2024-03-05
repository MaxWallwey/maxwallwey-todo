using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Todo.Api.Slices.Todo;

[ApiController]
public class AddToDoController : BaseController
{
    [ProducesResponseType(typeof(AddToDo.Response), StatusCodes.Status200OK)]
    [HttpPost("todo.add")]
    public async Task<AddToDo.Response> AddToDo([FromBody] AddToDo.AddToDoRequest request)
    {
        var id = await Mediator.Send(request);
        return id;
    }
    
    public AddToDoController(IMediator mediator) : base(mediator)
    {
    }
}