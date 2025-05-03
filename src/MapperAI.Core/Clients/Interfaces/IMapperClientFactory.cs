using MapperAI.Core.Clients.Models;

namespace MapperAI.Core.Clients.Interfaces;

public interface IMapperClientFactory
{
    IMapperClient CreateClient(MapperClientConfiguration configuration);
}