using System.Text.Json;
using MapperIA.Core.Serializers.Interfaces;

namespace MapperIA.Core.Serializers;

public class MapperSerializer : IMapperSerializer
{
    public string Serialize(object obj)
    {
        return JsonSerializer.Serialize(obj);
    }

    public T? Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json);
    }

}