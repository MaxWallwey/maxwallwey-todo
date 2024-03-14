using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Todo.Api.Domain.Infrastructure;
using Todo.Api.Domain.Todo;

namespace Todo.Api.Domain.Mongo;

public class MongoDbRepository<TDocument> : IDocumentRepository<TDocument>
    where TDocument : IDocument
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
        var todos = await Collection.AsQueryable()
            .Where(x => isComplete == null || x.IsComplete == isComplete)
            .ToListAsync();
        return todos;
    }

    public async Task<ToDoDocument?> FindOneToDoAsync(ObjectId id)
    {
        var document = await Collection.FindAsync(x => x.Id == id);
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

    public async Task UpdateToDoAsync(ToDoDocument document)
    {
        var filter = Builders<ToDoDocument>.Filter.Eq(x => x.Id, document.Id);

        await Collection.ReplaceOneAsync(filter, document);
    }

    public async Task<ObjectId> AddToDoAsync(ToDoDocument todo)
    {
        await Collection.InsertOneAsync(todo);

        return todo.Id;
    }

    public async Task RemoveToDoAsync(ObjectId id)
    {
        await Collection.DeleteOneAsync(x => x.Id == id);
    }
}