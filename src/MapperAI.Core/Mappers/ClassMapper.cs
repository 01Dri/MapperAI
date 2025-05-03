using MapperAI.Core.Clients.Interfaces;
using MapperAI.Core.Clients.Models;
using MapperAI.Core.Exceptions;
using MapperAI.Core.Mappers.Interfaces;
using MapperAI.Core.Serializers.Interfaces;

namespace MapperAI.Core.Mappers;

public class ClassMapper : IClassMapper
{
    private readonly IMapperSerializer _serializer;
    private readonly IMapperClientFactory _mapperClientFactory;
    private readonly MapperClientConfiguration _clientConfiguration;
    

    public ClassMapper(IMapperSerializer serializer, IMapperClientFactory mapperClientFactory, MapperClientConfiguration clientConfiguration)
    {
        _serializer = serializer;
        _mapperClientFactory = mapperClientFactory;
        _clientConfiguration = clientConfiguration;
    }


    public async Task<TK?> MapAsync<T, TK>(T origin, CancellationToken cancellationToken = default) where T : class where TK : class, new()
    {
        if (_clientConfiguration == null) throw new MapperException("Client configuration is null.");
        IMapperClient mapperClient = _mapperClientFactory.CreateClient(_clientConfiguration);
        TK destinyObject = new TK();
        string prompt = CreatePrompt(_serializer.Serialize(origin), destinyObject);
        var result = await mapperClient.SendAsync(prompt, cancellationToken);
        return _serializer.Deserialize<TK>(result.Value);
    }


    private string CreatePrompt<TK>(string originSerialized, TK destinyObject) where TK : class, new()
    {
        string destinyJson = _serializer.Serialize(destinyObject);

        return $"""
                You are a .NET developer AI assistant.
                Map the following object to match the structure of the target class.

                Origin object (JSON):
                {originSerialized}

                Target class structure (JSON):
                {destinyJson}

                Respond with only the mapped object in JSON format without explanations or code blocks.
                """;
    }


}