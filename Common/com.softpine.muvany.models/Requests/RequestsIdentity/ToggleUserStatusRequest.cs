namespace com.softpine.muvany.models.Requests;

/// <summary>
/// 
/// </summary>
public class ToggleUserStatusRequest
{
    /// <summary>
    /// 
    /// </summary>
    public bool? ActivateUser { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? UserId { get; set; }
}
