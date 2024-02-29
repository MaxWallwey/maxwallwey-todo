using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Domain;

namespace Todo.Api.Slices;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected BaseController(IMediator mediator)
    {
        Mediator = mediator;
    }

    protected IMediator Mediator { get; set; }
}