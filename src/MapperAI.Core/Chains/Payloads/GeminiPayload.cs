using MapperAI.Core.Chains.Interfaces;
using MapperAI.Core.Enums;

namespace MapperAI.Core.Chains.Payloads;

public class GeminiPayload : IPayloadHandler
{
    private IPayloadHandler? _next;

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
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            }
        };
    }

    public void SetNext(object next)
    {
        _next = (IPayloadHandler)next;
    }
}