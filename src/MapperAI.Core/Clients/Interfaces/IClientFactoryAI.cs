using MapperAI.Core.Clients.Models;

namespace MapperAI.Core.Clients.Interfaces;

public interface IClientFactoryAI
{
    IClientAI CreateClient(ClientConfiguration configuration);
}