namespace MapperAI.Core.Chains.Interfaces;

public interface IHandler<T>
{
    void SetNext(IHandler<T> next);
    
}