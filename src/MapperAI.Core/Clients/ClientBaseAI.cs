using MapperIA.Core.Clients.Models;
using MapperIA.Core.Serializers.Interfaces;

namespace MapperIA.Core.Clients;

public class ClientBaseAI
{
    protected ClientConfiguration ClientConfiguration;
    protected HttpClient HttpClient;
    protected IMapperSerializer Serializer;

    public ClientBaseAI(ClientConfiguration clientConfiguration,  IMapperSerializer serializer)
    {
        ClientConfiguration = clientConfiguration;
        HttpClient = new HttpClient();
        Serializer = serializer;
    }
}