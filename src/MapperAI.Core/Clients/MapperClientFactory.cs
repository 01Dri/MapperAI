using MapperAI.Core.Chains;
using MapperAI.Core.Clients.Interfaces;
using MapperAI.Core.Clients.Models;
using MapperAI.Core.Enums;
using MapperAI.Core.Serializers.Interfaces;

namespace MapperAI.Core.Clients;

public class MapperClientFactory(IMapperSerializer serializer, HttpClient httpClient) : IMapperClientFactory
{
    public IMapperClient CreateClient(MapperClientConfiguration configuration)
    {
        configuration.Endpoint = GetEndpoint(configuration);
        return new GenericMapperClient(configuration, serializer, httpClient, ChainConfigurator.GetHandlers());
    }

    private static string GetEndpoint(MapperClientConfiguration configuration)
    {
        return configuration.ModelType switch
        {
            ModelType.Gemini =>
                $"https://generativelanguage.googleapis.com/v1beta/models/{configuration.ModelVersion}:generateContent?key={configuration.ApiKey}",
            ModelType.Ollama => "http://localhost:11434/api/generate",
            _ => ""
        };
    }

}
