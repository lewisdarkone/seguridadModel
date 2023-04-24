using Ardalis.Specification;
using com.softpine.muvany.models.Tools;

namespace com.softpine.muvany.models.Specification;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TResult"></typeparam>
public class EntitiesByBaseFilterSpec<T, TResult> : Specification<T, TResult>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    public EntitiesByBaseFilterSpec(BaseFilter filter) =>
        Query.SearchBy(filter);
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class EntitiesByBaseFilterSpec<T> : Specification<T>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    public EntitiesByBaseFilterSpec(BaseFilter filter) =>
        Query.SearchBy(filter);
}
