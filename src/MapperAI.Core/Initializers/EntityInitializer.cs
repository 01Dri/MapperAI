﻿using System.Reflection;
using MapperIA.Core.Models;

namespace MapperIA.Core.Initializers;

public static class EntityInitializer
{

    public static BaseModelJson InitializeBaseModel<T>(string objJson) 
    {
        Type type = typeof(T);
        PropertyInfo[] properties = type.GetProperties();
        BaseModelJson baseModelJson = new BaseModelJson(objJson);

        foreach (var property in properties)
        {
            baseModelJson.Types.Add(new BaseTypes()
            {
                Name = property.Name,
                Type = property.PropertyType.ToString()
            });
        }

        return baseModelJson;
    }
    public static void CopyEntityProperties<T>(T? source, T destination)
    {
        var properties = typeof(T).GetProperties();
        foreach (var property in properties)
        {
            if (property.CanWrite)
            {
                var value = property.GetValue(source);
                property.SetValue(destination, value);
            }
        }
    }
}