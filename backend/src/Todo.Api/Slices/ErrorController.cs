using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Todo.Api.Slices;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : BaseController
{
    [Route("/error")]
    public IActionResult Action([FromServices] IHostEnvironment hostEnvironment)
    {
        if (!hostEnvironment.IsDevelopment())
        {
            return Problem();
        }

        var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

        return Problem(
            detail: exceptionHandlerFeature.Error.StackTrace,
            title: exceptionHandlerFeature.Error.Message);
    }

    public ErrorController(IMediator mediator) : base(mediator)
    {
    }
}