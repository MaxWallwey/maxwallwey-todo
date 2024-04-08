using System.Data;
using MongoDB.Driver;

namespace Todo.Api.Domain.Infrastructure;

public class DefaultRetryableExceptionFilter : IRetryableExceptionFilter
{
    public bool IsRetryable(Exception ex)
    {
        if (ex is AggregateException aggregateException)
        {
            return aggregateException.InnerExceptions.Any(IsRetryableExceptionType);
        }

        return IsRetryableExceptionType(ex);
    }

    private bool IsRetryableExceptionType(Exception ex)
    {
        return ex is MongoException || ex is DBConcurrencyException;
    }
}