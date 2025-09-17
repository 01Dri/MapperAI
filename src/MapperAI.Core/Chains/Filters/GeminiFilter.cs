using System.Text.Json;
using MapperAI.Core.Chains.Interfaces;
using MapperAI.Core.Enums;

namespace MapperAI.Core.Chains.Filters;

public class GeminiFilter : IFilterHandler
{

    private IFilterHandler? _next;


    public void SetNext(object next)
    {
        _next = (IFilterHandler)next;
    }
    public string? Filter(object obj, ModelType model)
    {
        if (model != ModelType.Gemini)
        {
            if (_next == null)
            {
                throw new ArgumentException("Cannot proceed: the next handler in the chain is null.");
            }

            return _next.Filter(obj, model);
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