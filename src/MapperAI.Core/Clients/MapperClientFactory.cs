using MapperAI.Core.Clients.Interfaces;
using MapperAI.Core.Clients.Models;
using MapperAI.Core.Enums;
using MapperAI.Core.Serializers.Interfaces;

namespace MapperAI.Core.Clients;

public class MapperClientFactory : IMapperClientFactory
{
    private readonly IMapperSerializer _serializer;
    private readonly HttpClient _httpClient;

    public MapperClientFactory(IMapperSerializer serializer, HttpClient httpClient)
    {
        _serializer = serializer;
        _httpClient = httpClient;
    }

    public IMapperClient CreateClient(MapperClientConfiguration configuration)
    {
        switch (configuration.Type)
        {
            case ModelType.Ollama:
                return new OllamaMapperClient(configuration, _serializer, _httpClient);
                       
            default:
                return new GeminiMapperClient(configuration, _serializer, _httpClient);
        }
    }
}