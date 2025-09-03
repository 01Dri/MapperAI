using System.ComponentModel;
using System.Reflection;

namespace MapperAI.Core.Extensions.Enums;

internal static class EnumExtension
{
    internal static string GetEnumDescriptionValue(this Enum data)
    {
        FieldInfo? field = data.GetType().GetField(data.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? throw new InvalidEnumArgumentException();
    } 
}