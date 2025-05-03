using MapperAI.Core.Clients.Interfaces;
using MapperAI.Core.Clients.Models;
using MapperAI.Core.Enums;
using MapperAI.Core.Serializers.Interfaces;

namespace MapperAI.Core.Clients;

public class MapperMapperClientFactory : IMapperClientFactory
{
    private readonly IMapperSerializer _serializer;

    public MapperMapperClientFactory(IMapperSerializer serializer)
    {
        _serializer = serializer;
    }

    public IMapperClient CreateClient(MapperClientConfiguration configuration)
    {
        switch (configuration.Type)
        {
            case ModelType.Ollama:
                return new OllamaMapperMapperClient(configuration, _serializer);
                       
            default:
                return new GeminiMapperMapperClient(configuration, _serializer);
        }
    }
}