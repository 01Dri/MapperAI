using MapperIA.Core.Clients.Models;
using MapperIA.Core.Mappers.Models;

namespace MapperIA.Core.Mappers.Interfaces;

public interface IFileMapper
{
    Task<List<ClassContent>> MapAsync
    (
        FileMapperConfiguration configuration,
        ClientConfiguration clientConfiguration,
        CancellationToken cancellationToken = default
    );
}