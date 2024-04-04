using MediatR;

namespace Todo.Api.Domain.Infrastructure;

public class UnitOfWorkBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkBehavior(IUnitOfWork unitOfWork) 
        => _unitOfWork = unitOfWork;
    
    public async Task<TResponse> Handle(TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        var response = await next();

        await _unitOfWork.Complete();

        return response;
    }
}