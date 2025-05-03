using MapperAI.Core.Clients.Models;

namespace MapperAI.Core.Mappers.Interfaces;

public interface IClassMapper
{
    Task<TK?> MapAsync<T, TK>(T origin, MapperClientConfiguration configuration, CancellationToken cancellationToken = default)
        where T : class
        where TK : class, new();
    
}