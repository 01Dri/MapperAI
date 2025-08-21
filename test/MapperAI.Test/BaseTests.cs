using MapperAI.Core.Clients;
using MapperAI.Core.Clients.Interfaces;
using MapperAI.Core.Serializers;
using MapperAI.Core.Serializers.Interfaces;

namespace MapperAI.Test;

public abstract class BaseTests
{
    public readonly IMapperSerializer Serializer;
    public readonly IMapperClientFactory Factory;

    public BaseTests()
    {
        Serializer = new MapperSerializer();
        Factory = new MapperClientFactory(Serializer, new HttpClient());
    }
}