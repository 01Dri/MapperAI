using MapperAI.Core.Clients.Models;
using MapperAI.Core.Mappers.Models;

namespace MapperAI.Core.Mappers.Interfaces;

public interface IFileMapper
{
    Task MapAsync
    (
        FileMapperConfiguration configuration,
        MapperClientConfiguration mapperClientConfiguration,
        CancellationToken cancellationToken = default
    );
}