using MapperAI.Core.Enums;

namespace MapperAI.Core.Chains.Interfaces;

public interface IPayloadHandler : IHandler<IPayloadHandler>
{
    object? CreatePayload(string prompt, ModelType model);
}