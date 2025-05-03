using MapperAI.Core.Clients.Models;

namespace MapperAI.Core.Clients.Interfaces;

public interface IMapperClient
{
    Task<MapperClientResponse> SendAsync(string prompt, CancellationToken cancellationToken);

}