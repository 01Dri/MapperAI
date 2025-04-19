using MapperIA.Core.Clients.Interfaces;
using MapperIA.Core.Clients.Models;
using MapperIA.Core.Mappers.Interfaces;
using MapperIA.Core.Mappers.Models;
using MapperIA.Core.Serializers.Interfaces;

namespace MapperIA.Core.Mappers;

public class FileMapper : IFileMapper
{

    private readonly IClientFactoryAI _iai;
    private readonly IMapperSerializer _serializer;

    public FileMapper(IClientFactoryAI iai, IMapperSerializer serializer)
    {
        _iai = iai;
        _serializer = serializer;
    }

    public Task<List<ClassContent>> MapAsync(FileMapperConfiguration configuration, ClientConfiguration clientConfiguration,
        CancellationToken cancellationToken = default)
    {
        IClientAI iai = _iai.CreateClient(clientConfiguration);
        throw new NotImplementedException();
    }
}