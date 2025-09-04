using System.Text.RegularExpressions;
using MapperAI.Core.Chains.Interfaces;
using MapperAI.Core.Enums;

namespace MapperAI.Core.Chains.Filters;

public class OllamaFilter : IFilterHandler
{
    
    private IHandler<IFilterHandler>? _next;
    
    public void SetNext(IHandler<IFilterHandler> next)
    {
        _next = next;
    }

    public string? Filter(object obj, ModelType model)
    {
        if (model != ModelType.Ollama)
        {
            if (_next == null)
            {
                throw new ArgumentException("Error");
            }
            var nextCasted =  (IFilterHandler)_next;
            return nextCasted.Filter(obj, model);
        }
        var objString = (string)obj;
        if (string.IsNullOrWhiteSpace(objString)) return string.Empty;
        string pattern = "<think>[\\s\\S]*?</think>";
        var cleaned = Regex.Replace(objString, pattern, string.Empty, RegexOptions.IgnoreCase);
        return cleaned.Trim();
    }
}