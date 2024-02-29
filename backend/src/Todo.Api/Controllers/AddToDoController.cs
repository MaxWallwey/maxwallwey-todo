using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Domain;
using Todo.Api.Models;
using Todo.Api.Slices;

namespace Todo.Api.Controllers;

[ApiController]
public class AddToDoController : BaseController
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpPost("todo.add")]
    public async Task<ResponseData<Guid>> AddToDo(CreateToDo model)
    {
        try
        {
            return await Mediator.Send(new AddToDo.AddToDoRequest(model));
        }
        catch (Exception e)
        {
            throw new BadHttpRequestException(e.Message);
        }
    }
    
    public AddToDoController(IMediator mediator) : base(mediator)
    {
    }
}