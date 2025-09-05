using MapperAI.Core.Enums;

namespace MapperAI.Core.Chains.Interfaces;

public interface IFilterHandler : IHandler
{
    
    string? Filter(object obj, ModelType model);
}


public interface IHandler 
{
    void SetNext(object next);
    
}
