using System.Text.Json;
using MapperAI.Core.Chains.Interfaces;
using MapperAI.Core.Enums;

namespace MapperAI.Core.Chains.Filters;

public class GeminiFilter : IFilterHandler
{

    private IHandler<IFilterHandler>? _next;


    public void SetNext(IHandler<IFilterHandler> next)
    {
        _next = next;
    }
    public string? Filter(object obj, ModelType model)
    {
        if (model != ModelType.GeminiFlash2_0)
        {
            if (_next == null)
            {
                throw new ArgumentException("Error");
            }
            var nextCasted =  (IFilterHandler)_next;
            return nextCasted.Filter(obj, model);
        }
        var objJson = (JsonDocument)obj;
        return objJson
            .RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString();
    }

}