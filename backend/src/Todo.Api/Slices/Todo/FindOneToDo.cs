using FluentValidation;
using MediatR;
using Todo.Api.Domain;

namespace Todo.Api.Slices.Todo;

public abstract class FindOneToDo
{
    public record FindOneToDoRequest(Guid Id) : IRequest<Response>;
    
    public record Response(ToDo Data);
    
    public class FindOneToDoHandler : IRequestHandler<FindOneToDoRequest, Response>
    {
        private readonly IToDoRepository _toDoRepository;

        public FindOneToDoHandler(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }
    
        public async Task<Response> Handle(FindOneToDoRequest request, CancellationToken cancellationToken)
        {
            var todo = await _toDoRepository.FindOneToDoAsync(request.Id);

            return new Response(todo!);
        }
    }
}