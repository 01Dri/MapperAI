using System.Text.Json;
using MapperAI.Core.Chains.Interfaces;
using MapperAI.Core.Clients.Interfaces;
using MapperAI.Core.Clients.Models;
using MapperAI.Core.Exceptions;
using MapperAI.Core.Serializers.Interfaces;

namespace MapperAI.Core.Clients;

internal class GenericMapperClient : MapperClientBase, IMapperClient
{
    private readonly IFilterHandler _filterHandler;
    private readonly IPayloadHandler _payloadHandler;

    public GenericMapperClient(MapperClientConfiguration mapperClientConfiguration, IMapperSerializer serializer, HttpClient httpClient, IFilterHandler filterHandler, IPayloadHandler payloadHandler) : base(mapperClientConfiguration, serializer, httpClient)
    {
        _filterHandler = filterHandler;
        _payloadHandler = payloadHandler;
    }


    public async Task<MapperClientResponse> SendAsync(string prompt, CancellationToken cancellationToken)
    {
        dynamic? payload = _payloadHandler.CreatePayload(prompt, MapperClientConfiguration.Type);
        if (payload == null) throw new MapperException("");
        using JsonDocument result = await GetAsync(MapperClientConfiguration.Endpoint, payload, cancellationToken);
        string? resultParsed = _filterHandler.Filter(result, MapperClientConfiguration.Type);
        if (resultParsed == null) throw new MapperException("Result request from AI is null.");
        string cleanedResult = resultParsed
            .Replace("```json", string.Empty)
            .Replace("```", string.Empty)
            .Replace("\\$", "\\\\$")
            .Trim();
        
        return new MapperClientResponse(cleanedResult);
    }
}