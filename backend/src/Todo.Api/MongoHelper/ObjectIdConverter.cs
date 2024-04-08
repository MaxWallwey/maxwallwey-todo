using System.Text.Json;
using System.Text.Json.Serialization;
using MongoDB.Bson;

namespace Todo.Api.MongoHelper;

public class ObjectIdConverter : JsonConverter<ObjectId>
{
    public override ObjectId Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var value = reader.GetString();

        return ObjectId.TryParse(value, out var result) ? result : ObjectId.Empty;
    }

    public override void Write(
        Utf8JsonWriter writer,
        ObjectId value,
        JsonSerializerOptions options)
    {
        var json = value == default
            ? string.Empty
            : value.ToString();

        writer.WriteStringValue(json);
    }
}