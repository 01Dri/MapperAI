namespace MapperIA.Core.Serializers.Interfaces;

public interface IMapperSerializer
{
    string Serialize(object obj);
    T? Deserialize<T>(string json);
}