using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Todo.Api.Slices.Todo;

[ApiController]
public class CompleteController : BaseController
{
    // Complete todo
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost("todo.complete")]
    public async Task CompleteToDo([FromQuery]CompleteToDo.CompleteToDoRequest request)
    {
        await Mediator.Send(request);
    }

    public CompleteController(IMediator mediator) : base(mediator)
    {
    }
}