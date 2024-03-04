using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Domain;

namespace Todo.Api.Slices.Todo;

[ApiController]
public class FindManyController : BaseController
{
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ToDo>))]
    [HttpGet("todo.findMany")]
    public async Task<FindManyToDo.Response> FindMany(bool? isComplete)
    {
        return await Mediator.Send(new FindManyToDo.FindManyToDoRequest(isComplete));
    }
    
    public FindManyController(IMediator mediator) : base(mediator)
    {
    }
}