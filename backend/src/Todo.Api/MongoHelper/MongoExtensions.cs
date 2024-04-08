using System.Reflection;
using System.Text.RegularExpressions;
using MongoDB.Driver;
using Todo.Api.Domain.Infrastructure;
using Todo.Api.Domain.Mongo;

namespace Todo.Api.MongoHelper;

public static class MongoExtensions
{
    public static void AddMongo(this IServiceCollection services)
    {
        RegisterIMongoCollection(services);
        RegisterIMongoDatabase(services);
    }

    public static void AddMongoCollection<TDocument>(this IServiceCollection services, string collectionName) =>
        services.AddSingleton(serviceProvider => serviceProvider.GetRequiredService<IMongoDatabase>()
            .GetCollection<TDocument>(collectionName));

    private static IReadOnlyDictionary<string, Type> GetCollectionNameToDocumentMappings()
    {
        var documentMappings = new Dictionary<string, Type>();
        var documentTypes = typeof(MongoConfigurator).Assembly.GetTypes()
            .Where(t => t.IsClass)
            .Where(t => t.Name.EndsWith("Document"))
            .Where(t => typeof(IDocument).IsAssignableFrom(t))
            .ToList();

        foreach (var documentType in documentTypes)
        {
            const string collectionNameGroupName = "CollectionName";
            var collectionNameMatch = Regex.Match(
                documentType.Name,
                $"^(?<{collectionNameGroupName}>.*)Document$");
            var collectionName = collectionNameMatch.Groups[collectionNameGroupName].Value;
            documentMappings.Add(
                string.Concat(collectionName[0].ToString().ToLowerInvariant(), collectionName.AsSpan(1)),
                documentType);
        }

        return documentMappings;
    }

    private static void RegisterIMongoCollection(IServiceCollection services)
    {
        var collectionNameToDocumentMappings = GetCollectionNameToDocumentMappings();

        var addMongoCollectionMethod =
            typeof(MongoExtensions).GetMethod(nameof(AddMongoCollection), BindingFlags.Public | BindingFlags.Static);

        foreach (var collectionNameToDocumentMapping in collectionNameToDocumentMappings)
        {
            var addMongoCollectionGenericMethod =
                addMongoCollectionMethod!.MakeGenericMethod(collectionNameToDocumentMapping.Value);
            addMongoCollectionGenericMethod.Invoke(null,
                new object[] { services, collectionNameToDocumentMapping.Key });
        }
    }

    private static void RegisterIMongoDatabase(IServiceCollection services) =>
        services.AddSingleton(serviceProvider =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var mongoOptions = configuration.GetSection(MongoOptions.Key).Get<MongoOptions>() ?? new MongoOptions();
            var mongoUrl = new MongoUrl(mongoOptions.ConnectionString);

            if (mongoUrl.DatabaseName == null)
            {
                throw new ArgumentException(
                    "The MongoDB connection string must contain a database name.",
                    mongoOptions.ConnectionString);
            }

            var mongoClient = new MongoClient(mongoUrl);
            var mongoDatabase = mongoClient.GetDatabase(mongoUrl.DatabaseName);

            return mongoDatabase;
        });
}