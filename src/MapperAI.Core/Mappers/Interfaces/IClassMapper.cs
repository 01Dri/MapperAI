using MapperIA.Core.Clients.Models;

namespace MapperIA.Core.Mappers.Interfaces;

public interface IClassMapper
{
    Task<TK?> MapAsync<T, TK>(T origin, ClientConfiguration configuration, CancellationToken cancellationToken = default)
        where T : class
        where TK : class, new();
    
}