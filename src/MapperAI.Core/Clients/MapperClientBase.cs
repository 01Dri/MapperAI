using System.Text;
using System.Text.Json;
using MapperAI.Core.Clients.Models;
using MapperAI.Core.Exceptions;
using MapperAI.Core.Serializers.Interfaces;

namespace MapperAI.Core.Clients;

internal abstract class MapperClientBase
{
    protected MapperClientConfiguration MapperClientConfiguration;
    private readonly HttpClient _httpClient;
    private readonly IMapperSerializer _serializer;

    protected MapperClientBase(MapperClientConfiguration mapperClientConfiguration,  IMapperSerializer serializer, HttpClient httpClient)
    {
        MapperClientConfiguration = mapperClientConfiguration;
        _serializer = serializer;
        _httpClient = httpClient;
    }

    protected async Task<JsonDocument> GetAsync(string endpoint, object body, CancellationToken cancellationToken = default)
    {
        try
        {
            string json = _serializer.Serialize(body);
            StringContent mediaTypeRequest = new(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, mediaTypeRequest, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                throw new MapperRequestStatusException($"Request failed with status: {response.StatusCode}");
            }

            string result = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonDocument.Parse(result);
        } 
        catch (Exception ex)
        {
            throw new MapperException($"An error occurred during processing: {ex.Message}");
        }

    }
    
}