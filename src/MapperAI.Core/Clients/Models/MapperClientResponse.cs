namespace MapperAI.Core.Clients.Models;

public class MapperClientResponse
{
    public MapperClientResponse(string value)
    {
        Value = value;
    }

    public string Value { get; }
}