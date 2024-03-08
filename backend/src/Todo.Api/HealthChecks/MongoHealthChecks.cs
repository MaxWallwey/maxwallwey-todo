using System.Diagnostics;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Todo.Api.HealthChecks;

public class MongoDbHealthCheck : IHealthCheck
{
    private readonly IMongoDatabase _database;

    public MongoDbHealthCheck(IMongoDatabase database)
    {
        _database = database;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            var stopwatch = Stopwatch.StartNew();

            await _database.RunCommandAsync(
                (Command<BsonDocument>)"{ ping: 1 }",
                cancellationToken: cancellationToken);

            stopwatch.Stop();

            return stopwatch.ElapsedMilliseconds > 1000
                ? HealthCheckResult.Degraded($"Elapsed ms {stopwatch.ElapsedMilliseconds}ms")
                : HealthCheckResult.Healthy();
        }
        catch (Exception exception)
        {
            return HealthCheckResult.Unhealthy("Failed to query MongoDb", exception);
        }

    }
}