using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Domain;
using Todo.Api.Domain.Models;

namespace Todo.Api.Slices.Todo;

[ApiController]
public class FindOneController : BaseController
{
    // List ToDo based on ID
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseData<ToDo>))]
    [HttpGet("todo.findOne")]
    public async Task<ResponseData<ToDo?>> FindOne(Guid id)
    {
        try
        {
            return await Mediator.Send(new FindOneToDo.FindOneToDoRequest(id));
        }
        catch (Exception e)
        {
            throw new BadHttpRequestException(e.Message);
        }
    }
    
    public FindOneController(IMediator mediator) : base(mediator)
    {
    }
}