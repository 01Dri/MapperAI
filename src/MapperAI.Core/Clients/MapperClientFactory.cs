using MapperAI.Core.Chains.Filters;
using MapperAI.Core.Chains.Interfaces;
using MapperAI.Core.Chains.Payloads;
using MapperAI.Core.Clients.Interfaces;
using MapperAI.Core.Clients.Models;
using MapperAI.Core.Enums;
using MapperAI.Core.Extensions.Utils;
using MapperAI.Core.Serializers.Interfaces;

namespace MapperAI.Core.Clients;

public class MapperClientFactory(IMapperSerializer serializer, HttpClient httpClient) : IMapperClientFactory
{
    private readonly List<IFilterHandler> _filterPrototypes = [ new GeminiFilter(), new OllamaFilter() ];
    private readonly List<IPayloadHandler> _payloadPrototypes = [ new GeminiPayload(), new OllamaPayload() ];

    public IMapperClient CreateClient(MapperClientConfiguration configuration)
    {
        var filterHandlersQueue = new Queue<IFilterHandler>(_filterPrototypes);
        var payloadHandlersQueue = new Queue<IPayloadHandler>(_payloadPrototypes);

        var firstFilterHandler = filterHandlersQueue.Dequeue();
        var firstPayloadHandler = payloadHandlersQueue.Dequeue();
        
        firstFilterHandler = SetNextHandlers(firstFilterHandler, null, filterHandlersQueue);
        firstPayloadHandler = SetNextHandlers(firstPayloadHandler, null, payloadHandlersQueue);

        configuration.Endpoint = GetEndpoint(configuration);
        return new GenericMapperClient(configuration, serializer, httpClient, firstFilterHandler, firstPayloadHandler);
    }

    private static string GetEndpoint(MapperClientConfiguration configuration)
    {
        return configuration.Type switch
        {
            ModelType.GeminiFlash2_0 =>
                $"https://generativelanguage.googleapis.com/v1beta/models/{configuration.Model}:generateContent?key={configuration.ApiKey}",
            ModelType.Ollama => "http://localhost:11434/api/generate",
            _ => ""
        };
    }

    private static T SetNextHandlers<T>(T firstHandler, T? lastHandler, Queue<T> handlers)
        where T : class, IHandler
    {
        var handler = handlers.DequeueOrDefault();
        if (handler == null)
            return firstHandler;

        if (lastHandler == null)
            firstHandler.SetNext(handler);
        else
            lastHandler.SetNext(handler);

        lastHandler = handler;
        return SetNextHandlers(firstHandler, lastHandler, handlers);
    }
}
