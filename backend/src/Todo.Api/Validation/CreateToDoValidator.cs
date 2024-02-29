using FluentValidation;
using Todo.Api.Slices;

namespace Todo.Api.Validation;

public class CreateToDoValidator : AbstractValidator<AddToDo.AddToDoRequest>
{
    public CreateToDoValidator()
    {
        RuleFor(x => x.Model.Name).NotNull().Length(3, 50);
    }
}