using MapperAI.Core.Clients.Models;

namespace MapperAI.Core.Mappers.Interfaces;

public interface IPDFMapper
{
    Task<T?> MapAsync<T>(string pdfPath, ClientConfiguration configuration, CancellationToken cancellationToken = default) 
        where T : class, new();

}