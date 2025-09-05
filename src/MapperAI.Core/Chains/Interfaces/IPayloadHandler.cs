using MapperAI.Core.Enums;

namespace MapperAI.Core.Chains.Interfaces;

public interface IPayloadHandler : IHandler
{
    object? CreatePayload(string prompt, ModelType model);
}