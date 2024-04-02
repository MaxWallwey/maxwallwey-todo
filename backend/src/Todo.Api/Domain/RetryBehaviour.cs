using System.Data;
using MediatR;
using MongoDB.Driver;
using Polly;

namespace Todo.Api.Domain;

public class RetryBehaviour<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        var concurrencyRetryPolicy = Policy.Handle<DBConcurrencyException>()
            .RetryAsync(5);

        var mongoConnectionRetryPolicy = Policy.Handle<MongoConnectionException>()
            .WaitAndRetryAsync(5, retryAttempt =>
                TimeSpan.FromMilliseconds(Math.Pow(2, retryAttempt) * 10));

        return await Policy.WrapAsync(concurrencyRetryPolicy, mongoConnectionRetryPolicy)
            .ExecuteAsync(async () => await next());
    }
}