using MapperIA.Core.Enums;

namespace MapperIA.Core.Clients.Models;

public class ClientConfiguration
{
    public string Model { get; set; }
    public string ApiKey { get; set; }
    public ModelType Type { get; set; }
}