

namespace com.softpine.muvany.models.Requests;

/// <summary>
/// 
/// </summary>
public class ChangePasswordRequest
{
    /// <summary>
    /// 
    /// </summary>
    public string Password { get; set; } = default!;
    /// <summary>
    /// 
    /// </summary>
    public string NewPassword { get; set; } = default!;
    /// <summary>
    /// 
    /// </summary>
    public string ConfirmNewPassword { get; set; } = default!;
}


