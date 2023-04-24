using com.softpine.muvany.models.Notifications;

namespace com.softpine.muvany.core.Notifications;

/// <summary>
/// 
/// </summary>
public class JobNotification : INotificationMessage
{
    /// <summary>
    /// 
    /// </summary>
    public string? Message { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? JobId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public decimal Progress { get; set; }
}
