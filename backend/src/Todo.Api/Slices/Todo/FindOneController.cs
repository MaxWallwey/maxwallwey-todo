using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Domain.Todo;

namespace Todo.Api.Slices.Todo;

[ApiController]
public class FindOneController : BaseController
{
    // List ToDo based on ID
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ToDoDocument))]
    [HttpGet("todo.findOne")]
    public async Task<FindOneToDo.Response> FindOne(Guid id)
    { 
        return await Mediator.Send(new FindOneToDo.FindOneToDoRequest(id));
    }
    
    public FindOneController(IMediator mediator) : base(mediator)
    {
    }
}