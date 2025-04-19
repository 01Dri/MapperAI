
namespace MapperAI.Core.Initializers;

public class BaseModelJson
{
    public string BaseJson { get; set; }
    public List<BaseTypes> Types { get; set; } = new List<BaseTypes>();

    public BaseModelJson(string baseJson)
    {
        BaseJson = baseJson;
    }
}