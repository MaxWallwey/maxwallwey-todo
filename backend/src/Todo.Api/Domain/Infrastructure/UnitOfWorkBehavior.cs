using MediatR;

namespace Todo.Api.Domain.Infrastructure;

public class UnitOfWorkBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkBehavior(
        ILogger<UnitOfWork> logger,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next();

        _logger.LogDebug("OUTBOX: Completing UnitOfWork for request of type {RequestType}.",
            typeof(TRequest).FullName);

        await _unitOfWork.Complete(cancellationToken);

        return response;
    }
}