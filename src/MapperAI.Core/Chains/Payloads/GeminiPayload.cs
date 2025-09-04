using MapperAI.Core.Chains.Interfaces;
using MapperAI.Core.Enums;

namespace MapperAI.Core.Chains.Payloads;

public class GeminiPayload : IPayloadHandler
{
    private IHandler<IPayloadHandler>? _next;

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

    public void SetNext(IHandler<IPayloadHandler> next)
    {
        _next = next;
    }
}