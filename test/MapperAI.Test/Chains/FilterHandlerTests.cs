using System.Text.Json;
using MapperAI.Core.Chains.Filters;
using MapperAI.Core.Enums;

namespace MapperAI.Test;

public class FilterHandlerTests
{
    [Fact]
    public void Should_Use_GeminiFilter_When_ModelType_Is_Gemini()
    {
        // Arrange
        var geminiFilter = new GeminiFilter();
        var ollamaFilter = new OllamaFilter();
        geminiFilter.SetNext(ollamaFilter);

        var json = "{\"candidates\":[{\"content\":{\"parts\":[{\"text\":\"test\"}]}}]}";
        var jsonDocument = JsonDocument.Parse(json);

        // Act
        var result = geminiFilter.Filter(jsonDocument, ModelType.Gemini);

        // Assert
        Assert.Equal("test", result);
    }

    [Fact]
    public void Should_Use_OllamaFilter_When_ModelType_Is_Ollama()
    {
        // Arrange
        var geminiFilter = new GeminiFilter();
        var ollamaFilter = new OllamaFilter();
        geminiFilter.SetNext(ollamaFilter);

        var text = "<think>thinking</think>test";

        // Act
        var result = geminiFilter.Filter(text, ModelType.Ollama);

        // Assert
        Assert.Equal("test", result);
    }
}
