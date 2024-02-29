using MediatR;
using Todo.Api.Domain;
using Todo.Api.Infrastructure;

namespace Todo.Api.Slices;

public abstract class RemoveToDo
{
    public record RemoveToDoRequest(Guid Id) : IRequest;

    public class RemoveToDoHandler : IRequestHandler<RemoveToDoRequest>
    {
        private readonly IDocumentRepository _documentRepository;

        public RemoveToDoHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }
    
        public Task Handle(RemoveToDoRequest request, CancellationToken cancellationToken)
        {
            return _documentRepository.RemoveToDoAsync(request.Id);
        }
    }
}