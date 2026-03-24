using MapperAI.Core.Chains.Filters;
using MapperAI.Core.Chains.Interfaces;
using MapperAI.Core.Chains.Payloads;
using MapperAI.Core.Enums;

namespace MapperAI.Core.Chains
{
    internal static class ChainConfigurator
    {
        public static IDictionary<HandlerType, IHandler> GetHandlers()
        {
            return new Dictionary<HandlerType, IHandler>
            {
                { HandlerType.Filter, BuildChain(GetFilters()) },
                { HandlerType.Payload, BuildChain(GetPayloads()) }
            };
        }

        private static T BuildChain<T>(IEnumerable<T> handlers)
            where T : class, IHandler
        {
            var handlerList = handlers.ToList();
            if (handlerList.Count == 0)
                throw new InvalidOperationException("No handlers provided to build the chain.");

            for (int i = 0; i < handlerList.Count - 1; i++)
            {
                handlerList[i].SetNext(handlerList[i + 1]);
            }

            return handlerList[0];
        }

        private static IEnumerable<IFilterHandler> GetFilters()
        {
            return new List<IFilterHandler>
            {
                new GeminiFilter(),
                new OllamaFilter()
            };
        }

        private static IEnumerable<IPayloadHandler> GetPayloads()
        {
            return new List<IPayloadHandler>
            {
                new GeminiPayload(),
                new OllamaPayload()
            };
        }
    }
}
