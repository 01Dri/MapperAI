using MapperIA.Core.Clients.Interfaces;
using MapperIA.Core.Clients.Models;
using MapperIA.Core.Mappers.Interfaces;
using MapperIA.Core.Serializers.Interfaces;

namespace MapperIA.Core.Mappers;

public class ClassMapper : IClassMapper
{
    private readonly IMapperSerializer _serializer;
    private readonly IClientFactoryAI _iai;

    public ClassMapper(IMapperSerializer serializer, IClientFactoryAI iai)
    {
        _serializer = serializer;
        _iai = iai;
    }


    public async Task<TK?> MapAsync<T, TK>(T origin, ClientConfiguration configuration, CancellationToken cancellationToken = default) where T : class where TK : class, new()
    {
        IClientAI iai = _iai.CreateClient(configuration);
        TK destinyObject = new TK();
        string prompt = CreatePrompt(_serializer.Serialize(origin), destinyObject);
        var result = await iai.SendAsync(prompt, cancellationToken);
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