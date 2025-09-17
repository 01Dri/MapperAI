using MapperAI.Core.Enums;
using MapperAI.Core.Extensions.Enums;

namespace MapperAI.Core.Clients.Models;

public class MapperClientConfiguration
{
    public string Endpoint { get; set; }
    public string? ApiKey { get; set; }
    public ModelType ModelType { get; set; }
    public string ModelVersion { get; set; }
    

    public MapperClientConfiguration(string? apiKey, ModelType modelType, string modelVersion)
    {
        ApiKey = apiKey;
        ModelType = modelType;
        ModelVersion = modelVersion;
    }
}