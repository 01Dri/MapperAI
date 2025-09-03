using System.Reflection;

namespace MapperAI.Core.Extensions.Initializers;

internal static  class InitializerExtension
{
    public static void Initialize(this object obj)
    {
        var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var genericProperties = properties.Where(x => x.PropertyType.IsGenericType);
        foreach (var property in genericProperties)
        {
            foreach (var itemType in property.PropertyType.GetGenericArguments())
            {
                InitializeDependencyProperties(itemType, property, obj);
            }
        }
    }
    
    private static void InitializeDependencyProperties(Type? itemType, PropertyInfo property, object obj)
    {
        if (itemType == null)
            throw new ArgumentNullException(nameof(itemType));

        var listType = typeof(List<>).MakeGenericType(itemType);
        var listInstance = Activator.CreateInstance(listType);

        var addMethod = listType.GetMethod("Add");
        if (addMethod == null)
            throw new InvalidOperationException("Not found 'Add' method in Generic List!");

        object? itemInstance;

        if (itemType == typeof(string))
        {
            itemInstance = "";
        }
        else
        {
            itemInstance = Activator.CreateInstance(itemType);
        }

        if (itemInstance != null)
        {
            addMethod.Invoke(listInstance, new[] { itemInstance });
        }

        property.SetValue(obj, listInstance);
    }
}