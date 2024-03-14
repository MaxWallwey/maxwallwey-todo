using FluentValidation;
using MediatR;
using MongoDB.Bson;
using Todo.Api.Authentication;
using Todo.Api.Domain.Infrastructure;
using Todo.Api.Domain.Todo;

namespace Todo.Api.Slices.Todo;

public abstract class RemoveToDo
{
    public record RemoveToDoRequest(ObjectId Id) : IRequest<Response>;

    public record Response;
    
    public class RemoveToDoValidator : AbstractValidator<RemoveToDoRequest>
    {
        public RemoveToDoValidator(IDocumentRepository<ToDoDocument> toDoRepository)
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
        private readonly IDocumentRepository<ToDoDocument> _documentRepository;
        private readonly IUserProfileAccessor _userProfileAccessor;

        public RemoveToDoHandler(IDocumentRepository<ToDoDocument> documentRepository, IUserProfileAccessor userProfileAccessor)
        {
            _documentRepository = documentRepository;
            _userProfileAccessor = userProfileAccessor;
        }
    
        public async Task<Response> Handle(RemoveToDoRequest request, CancellationToken cancellationToken)
        {
            var userId = _userProfileAccessor.Subject;
            
            var todo = await _documentRepository.FindOneToDoAsync(request.Id!);
            
            if (userId != todo!.UserId)
            {
                throw new UnauthorizedAccessException("You do not have access to this todo");
            }
            
            await _documentRepository.RemoveToDoAsync(request.Id);
            
            return new Response();
        }
    }
}