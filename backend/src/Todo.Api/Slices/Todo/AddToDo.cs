using MediatR;
using Todo.Api.Domain;
using Todo.Api.Infrastructure;
using Todo.Api.Models;

namespace Todo.Api.Slices;

public abstract class AddToDo
{
    public record AddToDoRequest(CreateToDo Model) : IRequest<ResponseData<Guid>>;

    public class AddToDoHandler : IRequestHandler<AddToDoRequest, ResponseData<Guid>>
    {
        private readonly IDocumentRepository _documentRepository;

        public AddToDoHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }
    
        public Task<ResponseData<Guid>> Handle(AddToDoRequest request, CancellationToken cancellationToken)
        {
            return _documentRepository.AddToDoAsync(request.Model);
        }
    }   
}