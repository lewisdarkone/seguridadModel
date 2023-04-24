namespace com.softpine.muvany.models.Interfaces;

/// <summary>
/// 
/// </summary>
public interface ISerializerService : ITransientService
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    string Serialize<T>(T obj);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    string Serialize<T>(T obj, Type type);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="text"></param>
    /// <returns></returns>
    T Deserialize<T>(string text);
}
