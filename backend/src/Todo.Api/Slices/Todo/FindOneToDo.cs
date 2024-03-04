using MediatR;
using Todo.Api.Domain;
using Todo.Api.Domain.Todo;
using Todo.Api.Infrastructure;
using Todo.Api.Domain.Models;

namespace Todo.Api.Slices.Todo;

public abstract class FindOneToDo
{
    public record FindOneToDoRequest(Guid Id) : IRequest<ResponseData<ToDoDocument?>>;

    public class FindOneToDoHandler : IRequestHandler<FindOneToDoRequest, ResponseData<ToDoDocument?>>
    {
        private readonly IDocumentRepository _documentRepository;

        public FindOneToDoHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }
    
        public Task<ResponseData<ToDoDocument?>> Handle(FindOneToDoRequest request, CancellationToken cancellationToken)
        {
            var todo = _documentRepository.FindOneToDoAsync(request.Id);

            return todo!;
        }
    }
}