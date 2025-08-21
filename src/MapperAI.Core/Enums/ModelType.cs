using System.ComponentModel;

namespace MapperAI.Core.Enums;

public enum ModelType
{
    [Description("gemini-2.0-flash")]
    GeminiFlash2_0,
    ChatGpt,
    Ollama
}