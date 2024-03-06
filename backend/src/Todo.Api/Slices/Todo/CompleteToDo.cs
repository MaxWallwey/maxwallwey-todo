using FluentValidation;
using MediatR;
using MongoDB.Bson;
using Todo.Api.Domain.Infrastructure;
using Todo.Api.Domain.Todo;
using Todo.Api.Infrastructure;

namespace Todo.Api.Slices.Todo;

public abstract class CompleteToDo
{
    public record CompleteToDoRequest(ObjectId Id) : IRequest<Response>;
    
    public record Response;
    
    public class CompleteToDoValidator : AbstractValidator<CompleteToDoRequest>
    {
        public CompleteToDoValidator(IDocumentRepository<ToDoDocument> toDoRepository)
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
        private readonly IDocumentRepository<ToDoDocument> _documentRepository;

        public CompleteToDoHandler(IDocumentRepository<ToDoDocument> documentRepository)
        {
            _documentRepository = documentRepository;
        }
    
        public async Task<Response> Handle(CompleteToDoRequest request, CancellationToken cancellationToken)
        {
            var document = await _documentRepository.FindOneToDoAsync(request.Id);
            
            document!.Complete();
            
            await _documentRepository.UpdateToDoAsync(document);
            
            return new Response();
        }
    }
}