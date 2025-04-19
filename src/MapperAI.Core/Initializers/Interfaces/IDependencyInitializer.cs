using System.Reflection;

namespace MapperAI.Core.Initializers.Interfaces;

public interface IDependencyInitializer
{
     void InitializeDependencyProperties(Type? itemType, PropertyInfo propertyInfo, object obj);
}