using System.Reflection;

namespace com.softpine.muvany.infrastructure.Extensions;

/// <summary>
/// 
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type"></param>
    /// <returns></returns>
    public static List<T> GetAllPublicConstantValues<T>(this Type type)
    {
        return type
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(T))
            .Select(x => x.GetRawConstantValue())
            .Where(x => x is not null)
            .Cast<T>()
            .ToList();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static List<string> GetNestedClassesStaticStringValues(this Type type)
    {
        var values = new List<string>();
        foreach (var prop in type.GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
        {
            object? propertyValue = prop.GetValue(null);
            if (propertyValue?.ToString() is string propertyString)
            {
                values.Add(propertyString);
            }
        }

        return values;
    }
}
