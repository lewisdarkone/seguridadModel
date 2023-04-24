using Hangfire.Client;
using Hangfire.Logging;
using Hangfire.Server;
using Hangfire.States;
using Hangfire.Storage;

namespace com.softpine.muvany.infrastructure.BackgroundJobs;

/// <summary>
/// 
/// </summary>
public class LogJobFilter : IClientFilter, IServerFilter, IElectStateFilter, IApplyStateFilter
{
    private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void OnCreating(CreatingContext context) =>
        Logger.InfoFormat("Creating a job based on method {0}...", context.Job.Method.Name);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void OnCreated(CreatedContext context) =>
        Logger.InfoFormat(
            "Job that is based on method {0} has been created with id {1}",
            context.Job.Method.Name,
            context.BackgroundJob?.Id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void OnPerforming(PerformingContext context) =>
        Logger.InfoFormat("Starting to perform job {0}", context.BackgroundJob.Id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void OnPerformed(PerformedContext context) =>
        Logger.InfoFormat("Job {0} has been performed", context.BackgroundJob.Id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void OnStateElection(ElectStateContext context)
    {
        if (context.CandidateState is FailedState failedState)
        {
            Logger.WarnFormat(
                "Job '{0}' has been failed due to an exception {1}",
                context.BackgroundJob.Id,
                failedState.Exception);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="transaction"></param>
    public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction) =>
        Logger.InfoFormat(
            "Job {0} state was changed from {1} to {2}",
            context.BackgroundJob.Id,
            context.OldStateName,
            context.NewState.Name);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="transaction"></param>
    public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction) =>
        Logger.InfoFormat(
            "Job {0} state {1} was unapplied.",
            context.BackgroundJob.Id,
            context.OldStateName);
}
