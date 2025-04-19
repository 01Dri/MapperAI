using MapperAI.Core.Clients.Interfaces;
using MapperAI.Core.Clients.Models;
using MapperAI.Core.Enums;
using MapperIA.Core.Serializers.Interfaces;

namespace MapperAI.Core.Clients;

public class ClientFactoryAI : IClientFactoryAI
{
    private readonly IMapperSerializer _serializer;

    public ClientFactoryAI(IMapperSerializer serializer)
    {
        _serializer = serializer;
    }

    public IClientAI CreateClient(ClientConfiguration configuration)
    {
        switch (configuration.Type)
        {
            case ModelType.Ollama:
                return new OllamaClientAI(configuration, _serializer);
                       
            default:
                return new GeminiClientAI(configuration, _serializer);
        }
    }
}