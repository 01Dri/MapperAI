using MapperAI.Core.Chains.Interfaces;
using MapperAI.Core.Clients.Models;
using MapperAI.Core.Enums;
using MapperAI.Core.Extensions.Enums;

namespace MapperAI.Core.Chains.Payloads;

public class OllamaPayload  : IPayloadHandler
{
    private IHandler<IPayloadHandler>? _next;

    public void SetNext(IHandler<IPayloadHandler> next)
    {
        _next = next;
    }
    public object? CreatePayload(string prompt, ModelType model)
    {
        if (model != ModelType.GeminiFlash2_0)
        {
            if (_next == null)
            {
                throw new ArgumentException("Error");
            }
            var nextCasted =  (IPayloadHandler)_next;
            return nextCasted.CreatePayload(prompt, model);
        }
        
        return new
        {
            model = model.GetEnumDescriptionValue(),
            prompt,
            stream = false
        }; 
    }
}