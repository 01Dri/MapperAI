using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using MapperAI.Core.Clients.Interfaces;
using MapperAI.Core.Clients.Models;
using MapperAI.Core.Exceptions;
using MapperIA.Core.Serializers.Interfaces;

namespace MapperAI.Core.Clients;

public class OllamaClientAI :  ClientBaseAI ,IClientAI
{
    private const string Endpoint = "http://localhost:11434/api/generate";

    public OllamaClientAI(ClientConfiguration clientConfiguration, IMapperSerializer serializer) : base(clientConfiguration, serializer)
    {
    }


    public async Task<ClientResponse> SendAsync(string prompt, CancellationToken cancellationToken)
    {
        var payload = new
        {
            model = ClientConfiguration.Model,
            prompt,
            stream = false
        };

        var json = Serializer.Serialize(payload);
        var mediaTypeRequest = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage httpResponse = await HttpClient.PostAsync(Endpoint, mediaTypeRequest, cancellationToken);
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new RequestStatusIAException($"Request failed with status: {httpResponse.StatusCode}");
            }

            string responseData = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            using var jsonDoc = JsonDocument.Parse(responseData);
            var content = jsonDoc.RootElement.GetProperty("response").GetString();
            string sanitized = RemoveThinkSections(content);

            return new ClientResponse { Value = sanitized ?? string.Empty };
        }
        catch (Exception ex) when (!(ex is RequestStatusIAException))
        {
            throw new ConverterIAException($"Erro ao processar a resposta da LLM local: {ex.Message}");
        }
    }
    private static string RemoveThinkSections(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        // Remove <think> tags and their content
        string pattern = "<think>[\\s\\S]*?</think>";
        var cleaned = Regex.Replace(input, pattern, string.Empty, RegexOptions.IgnoreCase);
        return cleaned.Trim();
    }
}