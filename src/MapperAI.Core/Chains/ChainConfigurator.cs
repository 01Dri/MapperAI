using MapperAI.Core.Chains.Filters;
using MapperAI.Core.Chains.Interfaces;
using MapperAI.Core.Chains.Payloads;
using MapperAI.Core.Enums;
using MapperAI.Core.Extensions.Utils;

namespace MapperAI.Core.Chains
{
    internal static class ChainConfigurator
    {
        public static IDictionary<HandlerType, IHandler> GetHandlers()
        {
            return new Dictionary<HandlerType, IHandler>
            {
                { HandlerType.Filter, GetConfiguredHandlers(GetFilters()) },
                { HandlerType.Payload, GetConfiguredHandlers(GetPayloads()) }
            };
        }

        private static T GetConfiguredHandlers<T>(Queue<T> handlers)
            where T : class, IHandler
        {
            var firstHandler = handlers.Dequeue();
            if (handlers.Count > 0)
            {
                return BuildHandlers(firstHandler, null, handlers);
            }
            return firstHandler;
        }
        private static T BuildHandlers<T>(T firstHandler, T? lastHandler, Queue<T> handlers)
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
            return BuildHandlers(firstHandler, lastHandler, handlers);
        }
        private static Queue<IFilterHandler> GetFilters()
        {
            return new Queue<IFilterHandler>(
    [
                new GeminiFilter(),
                new OllamaFilter()
              ]);
        }
        private static Queue<IPayloadHandler> GetPayloads()
        {
            return new Queue<IPayloadHandler>(
                [
                new GeminiPayload(),
                new OllamaPayload()
                ]);
        }
    } 
}
