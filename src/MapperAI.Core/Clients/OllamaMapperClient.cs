using System.Text.Json;
using System.Text.RegularExpressions;
using MapperAI.Core.Clients.Interfaces;
using MapperAI.Core.Clients.Models;
using MapperAI.Core.Exceptions;
using MapperAI.Core.Serializers.Interfaces;

namespace MapperAI.Core.Clients;

public class OllamaMapperClient :  MapperClientBase ,IMapperClient
{
    private const string Endpoint = "http://localhost:11434/api/generate";

    public OllamaMapperClient(MapperClientConfiguration mapperClientConfiguration, IMapperSerializer serializer) : base(mapperClientConfiguration, serializer)
    {
    }


    public async Task<MapperClientResponse> SendAsync(string prompt, CancellationToken cancellationToken)
    {
        dynamic payload = CreatePayload(prompt);
        using JsonDocument jsonDoc = await GetAsync(Endpoint, payload, cancellationToken);
        string? content = jsonDoc.RootElement.GetProperty("response").GetString();
        if (content == null) throw new MapperException("Result request from AI is null.");
        return new MapperClientResponse(RemoveThinkSections(content));
    }

    private dynamic CreatePayload(string promptText)
    {
        return new
        {
            model = MapperClientConfiguration.Model,
            promptText,
            stream = false
        }; 
    }
    private static string RemoveThinkSections(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        string pattern = "<think>[\\s\\S]*?</think>";
        var cleaned = Regex.Replace(input, pattern, string.Empty, RegexOptions.IgnoreCase);
        return cleaned.Trim();
    }
}