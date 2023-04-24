using com.softpine.muvany.models.Tools;

namespace com.softpine.muvany.models.Specification;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TResult"></typeparam>
public class EntitiesByPaginationFilterSpec<T, TResult> : EntitiesByBaseFilterSpec<T, TResult>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    public EntitiesByPaginationFilterSpec(PaginationFilter filter)
        : base(filter) =>
        Query.PaginateBy(filter);
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class EntitiesByPaginationFilterSpec<T> : EntitiesByBaseFilterSpec<T>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    public EntitiesByPaginationFilterSpec(PaginationFilter filter)
        : base(filter) =>
        Query.PaginateBy(filter);
}
