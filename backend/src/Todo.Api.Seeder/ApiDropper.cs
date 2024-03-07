using MongoDB.Driver;

namespace Todo.Api.Seeder;

public static class ApiDropper
{
    public static async Task DropAsync()
    {
        try
        {
            await DropDatabaseAsync();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
    
    private static async Task DropDatabaseAsync()
    {
        var mongoConnectionString = "mongodb://localhost:27017/todo-api";

        var mongoUrl = new MongoUrl(mongoConnectionString);

        if (mongoUrl.DatabaseName == null)
        {
            throw new ArgumentException("MongoDB connection string must contain a database name");
        }

        var mongoClient = new MongoClient(mongoConnectionString);

        await mongoClient.DropDatabaseAsync(mongoUrl.DatabaseName);

        Console.WriteLine($"Database '{mongoUrl.DatabaseName}' dropped.");
    }
}