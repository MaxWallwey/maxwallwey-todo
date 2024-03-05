using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Domain.Todo;

namespace Todo.Api.Slices.Todo;

[ApiController]
public class FindManyController : BaseController
{
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ToDoDocument>))]
    [HttpGet("todo.findMany")]
    public async Task<FindManyToDo.Response> FindMany([FromQuery]bool? isComplete)
    {
        return await Mediator.Send(new FindManyToDo.FindManyToDoRequest(isComplete));
    }
    
    public FindManyController(IMediator mediator) : base(mediator)
    {
    }
}