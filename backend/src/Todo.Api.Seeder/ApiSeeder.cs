using Bogus;
using Bogus.DataSets;
using MongoDB.Driver;
using Todo.Api.Sdk;

namespace Todo.Api.Seeder;

public class ApiSeeder
{
    private static IMongoCollection<ToDoDocument>? Collection { get; set; }

    public ApiSeeder(IMongoCollection<ToDoDocument>? collection)
    {
        Collection = collection;
    }

    public static async Task SeedAsync()
    {
        try
        {
            await SeedDatabaseAsync();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
    
    private static async Task SeedDatabaseAsync()
    {
        var mongoConnectionString = "mongodb://localhost:27017";

        var client = new MongoClient(mongoConnectionString);

        var database = client.GetDatabase("todo-api");
        
        Collection = database.GetCollection<ToDoDocument>("todo");

        var lorem = new Lorem();

        for (int i = 0; i < 5; i++)
        {
            var todo = new ToDoDocument(lorem.Sentences(1));
        
            await Collection.InsertOneAsync(todo);
        }

        Console.WriteLine("Database seeded.");
    }
}