using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.Notifications;

namespace com.softpine.muvany.models.Interfaces;

/// <summary>
/// 
/// </summary>
public interface INotificationSender : ITransientService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task BroadcastAsync(INotificationMessage notification, CancellationToken cancellationToken);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="excludedConnectionIds"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task BroadcastAsync(INotificationMessage notification, IEnumerable<string> excludedConnectionIds, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SendToAllAsync(INotificationMessage notification, CancellationToken cancellationToken);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="excludedConnectionIds"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SendToAllAsync(INotificationMessage notification, IEnumerable<string> excludedConnectionIds, CancellationToken cancellationToken);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="group"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SendToGroupAsync(INotificationMessage notification, string group, CancellationToken cancellationToken);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="group"></param>
    /// <param name="excludedConnectionIds"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SendToGroupAsync(INotificationMessage notification, string group, IEnumerable<string> excludedConnectionIds, CancellationToken cancellationToken);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="groupNames"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SendToGroupsAsync(INotificationMessage notification, IEnumerable<string> groupNames, CancellationToken cancellationToken);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SendToUserAsync(INotificationMessage notification, string userId, CancellationToken cancellationToken);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="userIds"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SendToUsersAsync(INotificationMessage notification, IEnumerable<string> userIds, CancellationToken cancellationToken);
}
