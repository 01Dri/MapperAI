using MapperAI.Core.Enums;

namespace MapperAI.Core.Clients.Models;

public class MapperClientConfiguration
{
    public required string Model { get; set; }
    public string? ApiKey { get; set; }
    public required ModelType Type { get; set; }
}