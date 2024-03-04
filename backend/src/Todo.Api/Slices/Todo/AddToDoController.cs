using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Todo.Api.Slices.Todo;

[ApiController]
public class AddToDoController : BaseController
{
    [ProducesResponseType(typeof(AddToDo.Response), StatusCodes.Status200OK)]
    [HttpPost("todo.add")]
    public async Task<AddToDo.Response> AddToDo(
        [FromBody] AddToDo.AddToDoRequest request,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    => await Mediator.Send(request, cancellationToken);
    
    public AddToDoController(IMediator mediator) : base(mediator)
    {
    }
}