namespace com.softpine.muvany.models.Contracts;

/// <summary>
/// 
/// </summary>
public interface ISoftDelete
{
    /// <summary>
    /// 
    /// </summary>
    DateTime? DeletedOn { get; set; }
    /// <summary>
    /// 
    /// </summary>
    string? DeletedBy { get; set; }
}
