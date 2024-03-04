using MediatR;
using Todo.Api.Domain;
using Todo.Api.Domain.Todo;
using Todo.Api.Infrastructure;
using Todo.Api.Domain.Models;

namespace Todo.Api.Slices.Todo;

public abstract class FindManyToDo
{
    public record FindManyToDoRequest(bool?? IsComplete) : IRequest<ResponseData<List<ToDoDocument>>?>;

    public class FindManyToDoHandler : IRequestHandler<FindManyToDoRequest, ResponseData<List<ToDoDocument>>?>
    {
        private readonly IDocumentRepository _documentRepository;

        public FindManyToDoHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }
    
        public Task<ResponseData<List<ToDoDocument>>> Handle(FindManyToDoRequest request, CancellationToken cancellationToken)
        {
            return _documentRepository.FindManyAsync(request.IsComplete);
        }
    }
}