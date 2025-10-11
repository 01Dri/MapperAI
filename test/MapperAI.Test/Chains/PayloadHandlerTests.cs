
using MapperAI.Core.Chains.Payloads;
using MapperAI.Core.Enums;

namespace MapperAI.Test.Chains;

public class PayloadHandlerTests : Utils.BaseTests
{
    [Fact]
    public void Should_Use_GeminiPayload_When_ModelType_Is_Gemini()
    {
        // Arrange
        var geminiPayload = new GeminiPayload();
        var ollamaPayload = new OllamaPayload();
        geminiPayload.SetNext(ollamaPayload);

        var prompt = "test prompt";

        // Act
        var result = geminiPayload.CreatePayload(prompt, ModelType.Gemini);

        // Assert
        Assert.NotNull(result);
        var json = Serializer.Serialize(result);
        Assert.Contains("test prompt", json);
    }

    [Fact]
    public void Should_Use_OllamaPayload_When_ModelType_Is_Ollama()
    {
        // Arrange
        var geminiPayload = new GeminiPayload();
        var ollamaPayload = new OllamaPayload();
        geminiPayload.SetNext(ollamaPayload);

        var prompt = "test prompt";

        // Act
        var result = geminiPayload.CreatePayload(prompt, ModelType.Ollama);

        // Assert
        Assert.NotNull(result);
        var json = Serializer.Serialize(result);
        Assert.Contains("test prompt", json);
    }
}
