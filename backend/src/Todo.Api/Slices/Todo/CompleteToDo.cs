using FluentValidation;
using MediatR;
using MongoDB.Bson;
using Todo.Api.Authentication;
using Todo.Api.Domain.Infrastructure;
using Todo.Api.Domain.Todo;

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
        private readonly IUserProfileAccessor _userProfileAccessor;

        public CompleteToDoHandler(IDocumentRepository<ToDoDocument> documentRepository, IUserProfileAccessor userProfileAccessor)
        {
            _documentRepository = documentRepository;
            _userProfileAccessor = userProfileAccessor;
        }
    
        public async Task<Response> Handle(CompleteToDoRequest request, CancellationToken cancellationToken)
        {
            var todo = await _documentRepository.FindOneToDoAsync(request.Id);
            
            var userId = _userProfileAccessor.Subject;

            if (todo!.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have access to this todo");
            }
            
            todo!.Complete();
            
            await _documentRepository.UpdateToDoAsync(todo);
            
            return new Response();
        }
    }
}