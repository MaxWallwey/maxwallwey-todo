using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Slices;

namespace Todo.Api.Slices.Todo;

[ApiController]
public class CompleteController : BaseController
{
    // Complete todo
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPost("todo.complete")]
    public async Task CompleteToDo(CompleteToDo.CompleteToDoRequest request)
    {
        await Mediator.Send(request);
    }
    public CompleteController(IMediator mediator) : base(mediator)
    {
    }
}