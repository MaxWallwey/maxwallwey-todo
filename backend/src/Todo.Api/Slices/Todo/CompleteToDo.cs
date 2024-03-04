using FluentValidation;
using MediatR;
using Todo.Api.Domain;

namespace Todo.Api.Slices.Todo;

public abstract class CompleteToDo
{
    public record CompleteToDoRequest(Guid Id) : IRequest<Response>;
    
    public record Response;
    
    public class CompleteToDoValidator : AbstractValidator<CompleteToDoRequest>
    {
        public CompleteToDoValidator(IToDoRepository toDoRepository)
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Please provide an ID for the todo.")
                .MustAsync(async (id, _) =>
                {
                    var todo = await toDoRepository.FindOneToDoAsync(id);
                    return todo != null;
                })
                .WithMessage("Todo was not found.");
        }
    }

    public class CompleteToDoHandler : IRequestHandler<CompleteToDoRequest, Response>
    {
        private readonly IToDoRepository _toDoRepository;

        public CompleteToDoHandler(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }
    
        public async Task<Response> Handle(CompleteToDoRequest request, CancellationToken cancellationToken)
        {
            await _toDoRepository.CompleteToDoAsync(request.Id);
            return new Response();
        }
    }
}