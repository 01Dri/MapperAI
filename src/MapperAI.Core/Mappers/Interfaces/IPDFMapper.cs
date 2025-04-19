using MapperIA.Core.Clients.Models;

namespace MapperIA.Core.Mappers.Interfaces;

public interface IPDFMapper
{
    Task<T?> MapAsync<T>(string pdfPath, ClientConfiguration configuration, CancellationToken cancellationToken = default) 
        where T : class, new();

}