using System.Text.Json;
using System.Text.Json.Serialization;
using MapperIA.Core.Serializers.Interfaces;

namespace MapperAI.Core.Serializers;

public class MapperSerializer : IMapperSerializer
{
    private JsonSerializerOptions? SerializerOptions { get; set; } = new ()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.Never,
        WriteIndented = true
    };
    public string Serialize(object obj)
    {
        return JsonSerializer.Serialize(obj);
    }

    public T? Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, SerializerOptions);

    }

}