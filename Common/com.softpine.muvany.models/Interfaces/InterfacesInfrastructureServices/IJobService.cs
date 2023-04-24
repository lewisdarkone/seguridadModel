using System.Linq.Expressions;

namespace com.softpine.muvany.models.Interfaces;

/// <summary>
/// 
/// </summary>
public interface IJobService : ITransientService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="methodCall"></param>
    /// <returns></returns>
    string Enqueue(Expression<Action> methodCall);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="methodCall"></param>
    /// <returns></returns>
    string Enqueue(Expression<Func<Task>> methodCall);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="methodCall"></param>
    /// <returns></returns>
    string Enqueue<T>(Expression<Action<T>> methodCall);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="methodCall"></param>
    /// <returns></returns>
    string Enqueue<T>(Expression<Func<T, Task>> methodCall);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="methodCall"></param>
    /// <param name="delay"></param>
    /// <returns></returns>
    string Schedule(Expression<Action> methodCall, TimeSpan delay);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="methodCall"></param>
    /// <param name="delay"></param>
    /// <returns></returns>
    string Schedule(Expression<Func<Task>> methodCall, TimeSpan delay);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="methodCall"></param>
    /// <param name="enqueueAt"></param>
    /// <returns></returns>
    string Schedule(Expression<Action> methodCall, DateTimeOffset enqueueAt);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="methodCall"></param>
    /// <param name="enqueueAt"></param>
    /// <returns></returns>
    string Schedule(Expression<Func<Task>> methodCall, DateTimeOffset enqueueAt);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="methodCall"></param>
    /// <param name="delay"></param>
    /// <returns></returns>
    string Schedule<T>(Expression<Action<T>> methodCall, TimeSpan delay);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="methodCall"></param>
    /// <param name="delay"></param>
    /// <returns></returns>
    string Schedule<T>(Expression<Func<T, Task>> methodCall, TimeSpan delay);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="methodCall"></param>
    /// <param name="enqueueAt"></param>
    /// <returns></returns>
    string Schedule<T>(Expression<Action<T>> methodCall, DateTimeOffset enqueueAt);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="methodCall"></param>
    /// <param name="enqueueAt"></param>
    /// <returns></returns>
    string Schedule<T>(Expression<Func<T, Task>> methodCall, DateTimeOffset enqueueAt);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="jobId"></param>
    /// <returns></returns>
    bool Delete(string jobId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="jobId"></param>
    /// <param name="fromState"></param>
    /// <returns></returns>
    bool Delete(string jobId, string fromState);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="jobId"></param>
    /// <returns></returns>
    bool Requeue(string jobId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="jobId"></param>
    /// <param name="fromState"></param>
    /// <returns></returns>
    bool Requeue(string jobId, string fromState);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="recurringJobId"></param>
    /// <param name="methodCall"></param>
    /// <param name="cronExpression"></param>
    /// <param name="timeZone"></param>
    /// <param name="queue"></param>
    void AddOrUpdate<T>(string recurringJobId, Expression<Func<T, Task>> methodCall, string cronExpression, TimeZoneInfo timeZone = null, string queue = "default");
}
