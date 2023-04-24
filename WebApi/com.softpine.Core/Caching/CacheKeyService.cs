using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.models.Interfaces;

namespace com.softpine.muvany.core.Services;

/// <summary>
/// 
/// </summary>
public interface ICacheKeyService : IScopedService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public string GetCacheKey(string name, object id);
}
