using MongoDB.Driver;

namespace Todo.Api.Mongo;

public class MongoDatabaseClient : IDatabaseClient
{
    private readonly IMongoDatabase _mongoDatabase;

    public MongoDatabaseClient(IMongoDatabase mongoDatabase)
    {
        _mongoDatabase = mongoDatabase;
    }

    public async Task DropDatabaseAsync(CancellationToken cancellationToken)
    {
        await _mongoDatabase.Client.DropDatabaseAsync(_mongoDatabase.DatabaseNamespace.DatabaseName, cancellationToken);
    }
}