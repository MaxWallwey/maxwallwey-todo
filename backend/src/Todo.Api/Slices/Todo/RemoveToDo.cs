using FluentValidation;
using MediatR;
using Todo.Api.Domain;
using Todo.Api.Infrastructure;

namespace Todo.Api.Slices.Todo;

public abstract class RemoveToDo
{
    public record RemoveToDoRequest(Guid Id) : IRequest<Response>;

    public record Response;
    
    public class RemoveToDoValidator : AbstractValidator<RemoveToDoRequest>
    {
        public RemoveToDoValidator(IToDoRepository toDoRepository)
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

    public class RemoveToDoHandler : IRequestHandler<RemoveToDoRequest, Response>
    {
        private readonly IDocumentRepository _documentRepository;

        public RemoveToDoHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }
    
        public async Task<Response> Handle(RemoveToDoRequest request, CancellationToken cancellationToken)
        {
            await _documentRepository.RemoveToDoAsync(request.Id);
            return new Response();
        }
    }
}