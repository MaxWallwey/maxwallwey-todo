using System.Data;
using MediatR;
using MongoDB.Driver;

namespace Todo.Api.Domain.Infrastructure;

public class RetryUnitOfWorkBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public RetryUnitOfWorkBehavior(IUnitOfWork unitOfWork) 
        => _unitOfWork = unitOfWork;
    
    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var retryCount = 0;

        while (true)
        {
            try
            {
                return next();
            }
            catch (MongoConnectionException exception)
            {
                if (retryCount >= 5)
                    throw;

                _unitOfWork.Reset();

                retryCount++;
            }
            catch (DBConcurrencyException exception)
            {
                if (retryCount >= 5)
                    throw;

                _unitOfWork.Reset();

                retryCount++;
            }
        }
    }
}