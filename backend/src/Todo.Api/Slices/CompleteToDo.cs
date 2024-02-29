using MediatR;
using Todo.Api.Domain;
using Todo.Api.Infrastructure;

namespace Todo.Api.Slices;

public abstract class CompleteToDo
{
    public record CompleteToDoRequest(Guid Id) : IRequest;

    public class CompleteToDoHandler : IRequestHandler<CompleteToDoRequest>
    {
        private readonly IDocumentRepository _documentRepository;

        public CompleteToDoHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }
    
        public Task Handle(CompleteToDoRequest request, CancellationToken cancellationToken)
        {
            return _documentRepository.CompleteToDoAsync(request.Id);
        }
    }
}