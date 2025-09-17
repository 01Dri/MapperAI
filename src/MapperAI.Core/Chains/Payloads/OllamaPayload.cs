using MapperAI.Core.Chains.Interfaces;
using MapperAI.Core.Enums;
using MapperAI.Core.Extensions.Enums;

namespace MapperAI.Core.Chains.Payloads;

public class OllamaPayload  : IPayloadHandler
{
    private IPayloadHandler? _next;

    public void SetNext(object next)
    {
        _next = (IPayloadHandler)next;
    }
    public object? CreatePayload(string prompt, ModelType model)
    {
        if (model != ModelType.Gemini)
        {
            if (_next == null)
            {
                throw new ArgumentException("Cannot proceed: the next handler in the chain is null.");
            }
            return _next.CreatePayload(prompt, model);
        }
        
        return new
        {
            model = model.GetEnumDescriptionValue(),
            prompt,
            stream = false
        }; 
    }
}