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

    private readonly Queue<IFilterHandler> _filterHandlers =
        new([new GeminiFilter(), new OllamaFilter()]);

    private readonly Queue<IPayloadHandler> _payloadHandlers =
        new([new GeminiPayload(), new OllamaPayload()]);


    public IMapperClient CreateClient(MapperClientConfiguration configuration)
    {
        var filterHandler = SetNextHandlers(_filterHandlers.Dequeue(), null);
        var payloadHandler = SetNextHandlers(_payloadHandlers.Dequeue(), null);
        configuration.Endpoint = GetEndpoint(configuration);
        return new GenericMapperClient(configuration, serializer, httpClient, filterHandler, payloadHandler);
    }

    private static string GetEndpoint(MapperClientConfiguration configuration)
    {
        switch (configuration.Type)
        {
            case ModelType.GeminiFlash2_0:
                return
                    $"https://generativelanguage.googleapis.com/v1beta/models/{configuration.Model}:generateContent?key={configuration.ApiKey}";
            case ModelType.Ollama:
                return "http://localhost:11434/api/generate";
            
            default:
                return "";
        }
    }

    private IFilterHandler SetNextHandlers(IFilterHandler firstHandler, IFilterHandler? lastHandler)
    {
        if (firstHandler == null) throw new ArgumentException("First instance of handler is required");
        var handler = _filterHandlers.DequeueOrDefault();
        if (handler == null)
        {
            return firstHandler;
        }

        if (lastHandler == null)
        {
            firstHandler.SetNext(handler);
        }
        else
        {
            lastHandler.SetNext(handler);
        }
        
        lastHandler = handler;
        
        return SetNextHandlers(firstHandler, lastHandler);

    }
    
    private IPayloadHandler SetNextHandlers(IPayloadHandler firstHandler, IPayloadHandler? lastHandler)
    {
        if (firstHandler == null) throw new ArgumentException("First instance of handler is required");
        var handler = _payloadHandlers.DequeueOrDefault();
        if (handler == null)
        {
            return firstHandler;
        }

        if (lastHandler == null)
        {
            firstHandler.SetNext(handler);
        }
        else
        {
            lastHandler.SetNext(handler);
        }
        
        lastHandler = handler;
        
        return SetNextHandlers(firstHandler, lastHandler);

    }
}
