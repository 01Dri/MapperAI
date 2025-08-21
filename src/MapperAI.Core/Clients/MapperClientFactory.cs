using MapperAI.Core.Clients.Interfaces;
using MapperAI.Core.Clients.Models;
using MapperAI.Core.Enums;
using MapperAI.Core.Serializers.Interfaces;

namespace MapperAI.Core.Clients;

public class MapperClientFactory(IMapperSerializer serializer, HttpClient httpClient) : IMapperClientFactory
{
    public IMapperClient CreateClient(MapperClientConfiguration configuration)
    {
        switch (configuration.Type)
        {
            case ModelType.Ollama:
                return new OllamaMapperClient(configuration, serializer, httpClient);
                       
            default:
                return new GeminiMapperClient(configuration, serializer, httpClient);
        }
    }
}