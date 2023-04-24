using System.Linq.Expressions;

using Hangfire;
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.models.Interfaces;

namespace com.softpine.muvany.infrastructure.BackgroundJobs;

/// <summary>
/// 
/// </summary>
public class HangfireService : IJobService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="jobId"></param>
    /// <returns></returns>
    public bool Delete(string jobId) =>
        BackgroundJob.Delete(jobId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="jobId"></param>
    /// <param name="fromState"></param>
    /// <returns></returns>
    public bool Delete(string jobId, string fromState) =>
        BackgroundJob.Delete(jobId, fromState);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="methodCall"></param>
    /// <returns></returns>
    public string Enqueue(Expression<Func<Task>> methodCall) =>
        BackgroundJob.Enqueue(methodCall);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="methodCall"></param>
    /// <returns></returns>
    public string Enqueue<T>(Expression<Action<T>> methodCall) =>
        BackgroundJob.Enqueue(methodCall);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="methodCall"></param>
    /// <returns></returns>
    public string Enqueue(Expression<Action> methodCall) =>
        BackgroundJob.Enqueue(methodCall);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="methodCall"></param>
    /// <returns></returns>
    public string Enqueue<T>(Expression<Func<T, Task>> methodCall) =>
        BackgroundJob.Enqueue(methodCall);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="jobId"></param>
    /// <returns></returns>
    public bool Requeue(string jobId) =>
        BackgroundJob.Requeue(jobId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="jobId"></param>
    /// <param name="fromState"></param>
    /// <returns></returns>
    public bool Requeue(string jobId, string fromState) =>
        BackgroundJob.Requeue(jobId, fromState);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="methodCall"></param>
    /// <param name="delay"></param>
    /// <returns></returns>
    public string Schedule(Expression<Action> methodCall, TimeSpan delay) =>
        BackgroundJob.Schedule(methodCall, delay);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="methodCall"></param>
    /// <param name="delay"></param>
    /// <returns></returns>
    public string Schedule(Expression<Func<Task>> methodCall, TimeSpan delay) =>
        BackgroundJob.Schedule(methodCall, delay);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="methodCall"></param>
    /// <param name="enqueueAt"></param>
    /// <returns></returns>
    public string Schedule(Expression<Action> methodCall, DateTimeOffset enqueueAt) =>
        BackgroundJob.Schedule(methodCall, enqueueAt);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="methodCall"></param>
    /// <param name="enqueueAt"></param>
    /// <returns></returns>
    public string Schedule(Expression<Func<Task>> methodCall, DateTimeOffset enqueueAt) =>
        BackgroundJob.Schedule(methodCall, enqueueAt);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="methodCall"></param>
    /// <param name="delay"></param>
    /// <returns></returns>
    public string Schedule<T>(Expression<Action<T>> methodCall, TimeSpan delay) =>
        BackgroundJob.Schedule(methodCall, delay);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="methodCall"></param>
    /// <param name="delay"></param>
    /// <returns></returns>
    public string Schedule<T>(Expression<Func<T, Task>> methodCall, TimeSpan delay) =>
        BackgroundJob.Schedule(methodCall, delay);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="methodCall"></param>
    /// <param name="enqueueAt"></param>
    /// <returns></returns>
    public string Schedule<T>(Expression<Action<T>> methodCall, DateTimeOffset enqueueAt) =>
        BackgroundJob.Schedule(methodCall, enqueueAt);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="methodCall"></param>
    /// <param name="enqueueAt"></param>
    /// <returns></returns>
    public string Schedule<T>(Expression<Func<T, Task>> methodCall, DateTimeOffset enqueueAt) =>
        BackgroundJob.Schedule(methodCall, enqueueAt);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="methodCall"></param>
    /// <returns></returns>
    public void AddOrUpdate<T>(string recurringJobId, Expression<Func<T, Task>> methodCall, string cronExpression, TimeZoneInfo timeZone = null, string queue = "default") =>
        RecurringJob.AddOrUpdate(recurringJobId, methodCall, cronExpression, timeZone, queue);
}
