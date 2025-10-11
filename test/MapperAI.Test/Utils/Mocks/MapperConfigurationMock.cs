using MapperAI.Core.Clients.Models;
using MapperAI.Core.Enums;

namespace MapperAI.Test.Utils.Mocks;

public static class MapperConfigurationMock
{
    public static MapperClientConfiguration GetConfig(ModelType modelType)
    {
        return modelType switch
        {
            ModelType.Gemini => new MapperClientConfiguration("AIzaSyDpGWRkZw53pjgI0rs5LkyT5H2PDJG1lAo", modelType, "gemini-2.0-flash"),
            ModelType.ChatGpt => new MapperClientConfiguration(Environment.GetEnvironmentVariable("CHATGPT_KEY"), modelType, "gpt-4"),
            ModelType.Ollama => new MapperClientConfiguration(null, modelType, "ollama"),
            _ => throw new ArgumentOutOfRangeException(nameof(modelType), modelType, null)
        };
    }
}