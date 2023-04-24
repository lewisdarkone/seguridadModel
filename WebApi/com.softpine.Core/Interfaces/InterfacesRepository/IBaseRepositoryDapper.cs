
namespace com.softpine.muvany.core.Interfaces;

/// <summary>
/// 
/// </summary>
public interface IBaseRepositoryDapper
{
    

    /// <summary>
    /// 
    /// </summary>
    void Dispose();

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <param name="database"></param>
    /// <returns></returns>
    Task<T> ExecuteSP<T>(string query, object param, string database = "");
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    Task<IEnumerable<T>> QueryTemplate<T>(string query);
}
