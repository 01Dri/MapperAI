using System.ComponentModel;

namespace MapperAI.Core.Enums;

public enum ModelType
{
    [Description("gemini-2.0-flash")]
    Gemini,
    ChatGpt,
    Ollama
}