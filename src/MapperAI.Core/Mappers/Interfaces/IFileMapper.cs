using MapperAI.Core.Mappers.Models;

namespace MapperAI.Core.Mappers.Interfaces;

public interface IFileMapper
{
    Task MapAsync
    (
        FileMapperConfiguration configuration,
        CancellationToken cancellationToken = default
    );
}