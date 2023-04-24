using com.softpine.muvany.core.Services;
using com.softpine.muvany.models.Contracts;

namespace com.softpine.muvany.core.Extensions;

/// <summary>
/// 
/// </summary>
public static class CacheKeyServiceExtensions
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="cacheKeyService"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string GetCacheKey<TEntity>(this ICacheKeyService cacheKeyService, object id)
    where TEntity : IEntity =>
        cacheKeyService.GetCacheKey(typeof(TEntity).Name, id);
}
