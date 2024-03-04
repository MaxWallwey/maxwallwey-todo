using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace Todo.Api.Validation;

public class ValidationExceptionFilter : IExceptionFilter
{
    private readonly ApiBehaviorOptions _apiBehaviorOptions;

    public ValidationExceptionFilter(
        IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        => _apiBehaviorOptions = apiBehaviorOptions?.Value ?? throw new ArgumentNullException(nameof(apiBehaviorOptions));

    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not ValidationException validationException)
        {
            return;
        }

        context.ExceptionHandled = true;

        context.ModelState.Clear();

        foreach (var error in validationException.Errors)
        {
            context.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

        context.Result = _apiBehaviorOptions.InvalidModelStateResponseFactory(context);
    }
}