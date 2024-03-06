namespace Todo.Api.Mongo;

public interface IDatabaseClient
{
    Task DropDatabaseAsync(CancellationToken cancellationToken);
}