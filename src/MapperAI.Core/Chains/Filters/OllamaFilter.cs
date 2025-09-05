using System.Text.RegularExpressions;
using MapperAI.Core.Chains.Interfaces;
using MapperAI.Core.Enums;

namespace MapperAI.Core.Chains.Filters;

public class OllamaFilter : IFilterHandler
{
    
    private IFilterHandler? _next;
    
    public void SetNext(object next)
    {
        _next = (IFilterHandler)next;
    }

    public string? Filter(object obj, ModelType model)
    {
        if (model != ModelType.Ollama)
        {
            if (_next == null)
            {
                throw new ArgumentException("Cannot proceed: the next handler in the chain is null.");
            }
            return _next.Filter(obj, model);
        }
        var objString = (string)obj;
        if (string.IsNullOrWhiteSpace(objString)) return string.Empty;
        string pattern = "<think>[\\s\\S]*?</think>";
        var cleaned = Regex.Replace(objString, pattern, string.Empty, RegexOptions.IgnoreCase);
        return cleaned.Trim();
    }
}