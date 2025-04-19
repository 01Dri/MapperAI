using MapperIA.Core.Clients.Models;

namespace MapperIA.Core.Clients.Interfaces;

public interface IClientFactoryAI
{
    IClientAI CreateClient(ClientConfiguration configuration);
}