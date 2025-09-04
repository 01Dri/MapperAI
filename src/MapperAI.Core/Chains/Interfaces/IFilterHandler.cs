using MapperAI.Core.Enums;

namespace MapperAI.Core.Chains.Interfaces;

public interface IFilterHandler : IHandler<IFilterHandler>
{
    string? Filter(object obj, ModelType model);
}