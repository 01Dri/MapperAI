using System.Reflection;

namespace MapperIA.Core.Initializers.Interfaces;

public interface IDependencyInitializer
{
     void InitializeDependencyProperties(Type? itemType, PropertyInfo propertyInfo, object obj);
}