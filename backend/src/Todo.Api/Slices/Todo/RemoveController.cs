using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Todo.Api.Slices.Todo;

[ApiController]
public class RemoveController : BaseController
{    
    // Remove todo
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpDelete("todo.remove")]
    public async Task RemoveToDo(RemoveToDo.RemoveToDoRequest request)
    {
        await Mediator.Send(request);
    }
    
    public RemoveController(IMediator mediator) : base(mediator)
    {
    }
}
