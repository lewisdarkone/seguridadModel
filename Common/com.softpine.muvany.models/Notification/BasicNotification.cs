#pragma warning disable 1591
namespace com.softpine.muvany.models.Notifications;

/// <summary>
/// 
/// </summary>
public class BasicNotification : INotificationMessage
{
    /// <summary>
    /// 
    /// </summary>
    public enum LabelType
    {
        Information,
        Success,
        Warning,
        Error
    }

    public string? Message { get; set; }
    public LabelType Label { get; set; }
}
#pragma warning restore 1591
