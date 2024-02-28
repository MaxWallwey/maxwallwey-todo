using FluentValidation;
using Todo.Api.Slices;

namespace Todo.Api.Validation;

public class CreateToDoValidator : AbstractValidator<AddToDoRequest>
{
    public CreateToDoValidator()
    {
        RuleFor(x => x.Model.Name).NotNull().Length(0, 50);
    }
}