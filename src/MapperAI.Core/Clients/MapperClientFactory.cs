using MapperAI.Core.Clients.Interfaces;
using MapperAI.Core.Clients.Models;
using MapperAI.Core.Enums;
using MapperAI.Core.Serializers.Interfaces;

namespace MapperAI.Core.Clients;

public class MapperClientFactory : IClientFactoryAI
{
    private readonly IMapperSerializer _serializer;

    public MapperClientFactory(IMapperSerializer serializer)
    {
        _serializer = serializer;
    }

    public IClientAI CreateClient(MapperClientConfiguration configuration)
    {
        switch (configuration.Type)
        {
            case ModelType.Ollama:
                return new OllamaMapperClient(configuration, _serializer);
                       
            default:
                return new GeminiMapperClient(configuration, _serializer);
        }
    }
}