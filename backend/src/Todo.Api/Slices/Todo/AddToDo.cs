using FluentValidation;
using MediatR;
using Todo.Api.Domain;

namespace Todo.Api.Slices.Todo;

public abstract class AddToDo
{
    public record AddToDoRequest(string Name) : IRequest<Response>;
    
    public record Response(Guid Data);
    
    public class CreateToDoValidator : AbstractValidator<AddToDoRequest>
    {
        public CreateToDoValidator(IToDoRepository toDoRepository)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Please provide a name for the todo.")
                .MustAsync(async (name, _) => name != null && !await toDoRepository.AnyAsync(name))
                .WithMessage("Name must be unique.");
        }
    }

    public class AddToDoHandler : IRequestHandler<AddToDoRequest, Response>
    {
        private readonly IToDoRepository _toDoRepository;

        public AddToDoHandler(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }
        public async Task<Response> Handle(AddToDoRequest request, CancellationToken cancellationToken)
        {
            var newTodo = await _toDoRepository.AddToDoAsync(request.Name);
            return new Response(newTodo);
        }
    }   
}