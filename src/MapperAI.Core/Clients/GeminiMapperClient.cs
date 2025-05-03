using System.Text.Json;
using MapperAI.Core.Clients.Interfaces;
using MapperAI.Core.Clients.Models;
using MapperAI.Core.Exceptions;
using MapperAI.Core.Serializers.Interfaces;

namespace MapperAI.Core.Clients;

public class GeminiMapperClient : MapperClientBase, IClientAI
{
  private const string EndpointBase = "https://generativelanguage.googleapis.com/v1beta";

  public GeminiMapperClient(MapperClientConfiguration mapperClientConfiguration, IMapperSerializer serializer) : base(
    mapperClientConfiguration, serializer)
  {
  }

  public async Task<MapperClientResponse> SendAsync(string prompt, CancellationToken cancellationToken)
  {
    dynamic payload = CreatePayload(prompt);
    string endpoint =
      $"{EndpointBase}/models/{MapperClientConfiguration.Model}:generateContent?key={MapperClientConfiguration.ApiKey}";
    using JsonDocument result = await GetAsync(endpoint, payload, cancellationToken);
    string? resultParsed = ParseResponse(result);
    if (resultParsed == null) throw new MapperException("Result request from AI is null.");
    string cleanedResult = resultParsed
      .Replace("```json", string.Empty)
      .Replace("```", string.Empty)
      .Replace("\\$", "\\\\$")
      .Trim();

    return new MapperClientResponse(cleanedResult);
  }

  private dynamic CreatePayload(string promptText)
  {
    return new
    {
      contents = new[]
      {
        new
        {
          parts = new[]
          {
            new { text = promptText }
          }
        }
      }
    };
}


private string? ParseResponse(JsonDocument response)
  => response
    .RootElement
    .GetProperty("candidates")[0]
    .GetProperty("content")
    .GetProperty("parts")[0]
    .GetProperty("text")
    .GetString();
  
  
}