using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Todo.Api.Domain.Todo;
using Todo.Api.Infrastructure;

namespace Todo.Api.Mongo;

public class MongoDbRepository : IDocumentRepository
{
    private IMongoCollection<ToDoDocument> Collection { get; }

    public MongoDbRepository(IConfiguration configuration)
    {
        var client = new MongoClient(configuration["Mongo:ConnectionString"]);
        var database = client.GetDatabase("todo-api");
        Collection = database.GetCollection<ToDoDocument>("todo");
    }
    
    public async Task<List<ToDoDocument>?> FindManyAsync(bool? isComplete)
    {
        return await Collection.AsQueryable()
            .Where(x => isComplete == null || x.IsComplete == isComplete)
            .ToListAsync();
    }

    public async Task<ToDoDocument?> FindOneToDoAsync(string id)
    {
        var document = await Collection.FindAsync(x => x.Id == ObjectId.Parse(id));
        return document.SingleOrDefault();
    }

    public Task<bool> AnyAsync(string name)
    {
        var todo = Collection.AsQueryable().SingleOrDefault(x => x.Name == name);
            
        if (todo != null)
        {
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }

    public async Task CompleteToDoAsync(string id)
    {
        var document = await FindOneToDoAsync(id);
        
        document!.Complete();
    }

    public async Task<string> AddToDoAsync(string name)
    {
        var todo = new ToDoDocument(name);
        
        await Collection.InsertOneAsync(todo);

        return todo.Id.ToString();
    }

    public async Task RemoveToDoAsync(string id)
    {
        await Collection.DeleteOneAsync(x => x.Id == ObjectId.Parse(id));
    }
}