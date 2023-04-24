namespace com.softpine.muvany.models.Tools;

/// <summary>
/// 
/// </summary>
public class PaginationFilter : BaseFilter
{
    /// <summary>
    /// 
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int PageSize { get; set; } = int.MaxValue;

    /// <summary>
    /// 
    /// </summary>
    public string[]? OrderBy { get; set; }
}

/// <summary>
/// 
/// </summary>
public static class PaginationFilterExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static bool HasOrderBy(this PaginationFilter filter) =>
        filter.OrderBy?.Any() is true;
}
