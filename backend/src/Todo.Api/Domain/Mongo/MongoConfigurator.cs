using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using Todo.Api.Domain.Infrastructure;

namespace Todo.Api.Domain.Mongo;

public static class MongoConfigurator
{
    private static bool s_hasRun;
    private static readonly object s_lock;

    static MongoConfigurator()
    {
        s_hasRun = false;
        s_lock = new object();
    }

    public static void Configure(string conventionName)
    {
        if (s_hasRun)
        {
            return;
        }

        lock (s_lock)
        {
            if (!s_hasRun)
            {
                RegisterConventions(conventionName);
                RegisterClassMaps();
                RegisterSerializers();

                s_hasRun = true;
            }
        }
    }

    private static void RegisterClassMaps()
    {
        var registerClassMapMethod =
            typeof(BsonClassMap).GetMethod(nameof(BsonClassMap.RegisterClassMap), Array.Empty<Type>());

        if (registerClassMapMethod == null)
        {
            return;
        }

        var documentTypes = typeof(MongoConfigurator).Assembly.GetTypes()
            .Where(t => t.IsClass)
            .Where(t => t.Name.EndsWith("Document"))
            .Where(t => typeof(IDocument).IsAssignableFrom(t))
            .ToList();

        foreach (var documentType in documentTypes)
        {
            var registerClassMapGenericMethod = registerClassMapMethod.MakeGenericMethod(documentType);
            registerClassMapGenericMethod.Invoke(null, null);
        }
    }

    private static void RegisterConventions(string conventionName)
    {
        var conventionPack = new ConventionPack
        {
            new CamelCaseElementNameConvention(),
            new EnumRepresentationConvention(BsonType.String),
            new IgnoreExtraElementsConvention(true),
            new IgnoreIfNullConvention(true)
        };

        ConventionRegistry.Remove(conventionName);
        ConventionRegistry.Register(conventionName, conventionPack, t => true);
    }

    private static void RegisterSerializers()
    {
        BsonSerializer.RegisterSerializer(typeof(DateTimeOffset), new DateTimeOffsetSerializer(BsonType.DateTime));
        BsonSerializer.RegisterSerializer(typeof(DateTimeOffset?),
            new NullableSerializer<DateTimeOffset>(new DateTimeOffsetSerializer(BsonType.DateTime)));

        BsonSerializer.RegisterSerializer(typeof(decimal),
            new DecimalSerializer(BsonType.Decimal128, new RepresentationConverter(false, true)));
        BsonSerializer.RegisterSerializer(typeof(decimal?),
            new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));
    }
}