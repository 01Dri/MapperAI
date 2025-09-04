using MapperAI.Core.Enums;
using MapperAI.Core.Extensions.Enums;

namespace MapperAI.Core.Clients.Models;

public class MapperClientConfiguration
{
    public string Endpoint { get; set; }
    public string? ApiKey { get; set; }
    public ModelType Type { get; set; }
    public string Model => Type.GetEnumDescriptionValue();
    
    public MapperClientConfiguration( string? apiKey, ModelType type)
    {
        ApiKey = apiKey;
        Type = type;
    }

}