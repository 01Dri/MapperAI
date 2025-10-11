using MapperAI.Core.Clients;
using MapperAI.Core.Clients.Interfaces;
using MapperAI.Core.Serializers;
using MapperAI.Core.Serializers.Interfaces;
using DotNetEnv;

namespace MapperAI.Test.Utils;

public abstract class BaseTests
{
    public readonly IMapperSerializer Serializer;
    public readonly IMapperClientFactory Factory;

    public BaseTests()
    {
        Env.Load();
        Serializer = new MapperSerializer();
        Factory = new MapperClientFactory(Serializer, new HttpClient());
    }
}