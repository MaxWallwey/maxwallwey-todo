namespace Todo.Api.Domain.Infrastructure;

public interface IRetryableExceptionFilter
{
    bool IsRetryable(Exception ex);
}