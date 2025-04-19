using MapperAI.Core.Clients.Models;

namespace MapperAI.Core.Clients.Interfaces;

public interface IClientAI
{
    Task<ClientResponse> SendAsync(string prompt, CancellationToken cancellationToken);
}