using System.Text.Json;
using MapperAI.Core.Chains.Interfaces;
using MapperAI.Core.Clients.Interfaces;
using MapperAI.Core.Clients.Models;
using MapperAI.Core.Enums;
using MapperAI.Core.Exceptions;
using MapperAI.Core.Serializers.Interfaces;

namespace MapperAI.Core.Clients;

internal class GenericMapperClient : MapperClientBase, IMapperClient
{
    private readonly IDictionary<HandlerType, IHandler> _handlers;

    public GenericMapperClient(MapperClientConfiguration mapperClientConfiguration, IMapperSerializer serializer, HttpClient httpClient, IDictionary<HandlerType, IHandler> handlers) : base(mapperClientConfiguration, serializer, httpClient)
    {
        _handlers = handlers;
    }

    public async Task<MapperClientResponse> SendAsync(string prompt, CancellationToken cancellationToken)
    {
        if (!_handlers.TryGetValue(HandlerType.Payload, out IHandler? payloadHandler) || payloadHandler is not IPayloadHandler _payloadHandler)
        {
            throw new MapperException("Payload handler not found.");
        }
        if (!_handlers.TryGetValue(HandlerType.Filter, out IHandler? filterHandler) || filterHandler is not IFilterHandler _filterHandler)
        {
            throw new MapperException("Filter handler not found.");
        }
        dynamic? payload = _payloadHandler.CreatePayload(prompt, MapperClientConfiguration.ModelType);
        if (payload == null) throw new MapperException("");
        using JsonDocument result = await GetAsync(MapperClientConfiguration.Endpoint, payload, cancellationToken);
        string? resultParsed = _filterHandler.Filter(result, MapperClientConfiguration.ModelType);
        if (resultParsed == null) throw new MapperException("Result request from AI is null.");
        string cleanedResult = resultParsed
            .Replace("```json", string.Empty)
            .Replace("```", string.Empty)
            .Replace("\\$", "\\\\$")
            .Trim();
        
        return new MapperClientResponse(cleanedResult);
    }
}