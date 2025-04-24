using System.Text;
using System.Text.Json;
using MapperAI.Core.Clients.Interfaces;
using MapperAI.Core.Clients.Models;
using MapperAI.Core.Exceptions;
using MapperIA.Core.Serializers.Interfaces;

namespace MapperAI.Core.Clients;

public class GeminiClientAI : ClientBaseAI ,IClientAI
{
  private const string EndpointBase = "https://generativelanguage.googleapis.com/v1beta";

  public GeminiClientAI(ClientConfiguration clientConfiguration, IMapperSerializer serializer) : base(clientConfiguration, serializer)
  {
  }

  public async Task<ClientResponse> SendAsync(string prompt, CancellationToken cancellationToken)
    {
      var payload = new
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
        var payloadJson = Serializer.Serialize(payload);
        var mediaTypeRequest = new StringContent(payloadJson, Encoding.UTF8, "application/json");
        var endpoint = $"{EndpointBase}/models/{ClientConfiguration.Model}:generateContent?key={ClientConfiguration.ApiKey}";

        try
        {
          var response = await HttpClient.PostAsync(endpoint, mediaTypeRequest, cancellationToken);
          if (!response.IsSuccessStatusCode)
          {
            throw new RequestStatusIAException($"Request failed with status: {response.StatusCode}");
          }
          
          var result = await response.Content.ReadAsStringAsync();
          using var doc = JsonDocument.Parse(result);
          var text = doc
            .RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString();

          if (text == null) throw new Exception("Error");
          var cleanedJson = text
            .Replace("```json", string.Empty)
            .Replace("```", string.Empty)
            .Replace("\\$", "\\\\$") 
            .Trim();

          // var textSanitized = SanitizeJson(text);
          return new ClientResponse { Value = cleanedJson};
        }
        catch (Exception ex)
        {
          throw new ConverterIAException($"An error occurred during processing: {ex.Message}");
        }
    }
}