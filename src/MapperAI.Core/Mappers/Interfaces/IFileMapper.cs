using MapperAI.Core.Clients.Models;
using MapperAI.Core.Mappers.Models;

namespace MapperAI.Core.Mappers.Interfaces;

public interface IFileMapper
{
    Task MapAsync
    (
        FileMapperConfiguration configuration,
        ClientConfiguration clientConfiguration,
        CancellationToken cancellationToken = default
    );
}