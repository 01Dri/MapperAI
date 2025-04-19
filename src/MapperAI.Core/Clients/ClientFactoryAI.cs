using MapperIA.Core.Clients.Interfaces;
using MapperIA.Core.Clients.Models;
using MapperIA.Core.Serializers.Interfaces;

namespace MapperIA.Core.Clients;

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
            default:
                return new GeminiClientAI(configuration, _serializer);
        }
    }
}