namespace com.softpine.muvany.infrastructure.OpenApi;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class SwaggerHeaderAttribute : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    public string HeaderName { get; }
    /// <summary>
    /// 
    /// </summary>
    public string? Description { get; }
    /// <summary>
    /// 
    /// </summary>
    public string? DefaultValue { get; }
    /// <summary>
    /// 
    /// </summary>
    public bool IsRequired { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="headerName"></param>
    /// <param name="description"></param>
    /// <param name="defaultValue"></param>
    /// <param name="isRequired"></param>
    public SwaggerHeaderAttribute(string headerName, string? description = null, string? defaultValue = null, bool isRequired = false)
    {
        HeaderName = headerName;
        Description = description;
        DefaultValue = defaultValue;
        IsRequired = isRequired;
    }
}
