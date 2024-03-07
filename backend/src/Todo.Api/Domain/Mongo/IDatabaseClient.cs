namespace Todo.Api.Domain.Mongo;

public interface IDatabaseClient
{
    Task DropDatabaseAsync(CancellationToken cancellationToken);
}