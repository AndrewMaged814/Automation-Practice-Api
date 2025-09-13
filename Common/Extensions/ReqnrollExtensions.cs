using System;
using Reqnroll;

namespace Common.Extensions;

public static class ReqnrollExtensions
{
    public static T ConvertTo<T>(this Table table) where T : class
    {
        var instance = table.CreateInstance<T>();
        ReplaceNullStrings(instance);
        return instance;
    }

    private static void ReplaceNullStrings<T>(T obj) where T : class
    {
        var properties = typeof(T).GetProperties();
        foreach (var property in properties)
        {
            if (property.PropertyType != typeof(string)) continue;
            if (property.GetValue(obj) is string value &&
                (value.Equals("null", StringComparison.OrdinalIgnoreCase) ||
                 string.IsNullOrWhiteSpace(value)))
                property.SetValue(obj, null);
        }
    }
}