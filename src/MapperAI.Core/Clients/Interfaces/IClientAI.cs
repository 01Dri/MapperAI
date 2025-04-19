using MapperIA.Core.Clients.Models;

namespace MapperIA.Core.Clients.Interfaces;

public interface IClientAI
{
    Task<ClientResponse> SendAsync(string prompt, CancellationToken cancellationToken);
}