using MapperAI.Core.Enums;

namespace MapperAI.Core.Clients.Models;

public class MapperClientConfiguration
{
    public string Model { get; set; }
    public string? ApiKey { get; set; }
    public ModelType Type { get; set; }

    public MapperClientConfiguration(string model, string? apiKey, ModelType type)
    {
        Model = model;
        ApiKey = apiKey;
        Type = type;
    }

    public MapperClientConfiguration(string model, ModelType type)
    {
        Model = model;
        Type = type;
    }
}