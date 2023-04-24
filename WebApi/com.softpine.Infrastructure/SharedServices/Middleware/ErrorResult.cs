namespace com.softpine.muvany.infrastructure.Shared.Middleware;

/// <summary>
/// 
/// </summary>
public class ErrorResult
{
    /// <summary>
    /// 
    /// </summary>
    public List<string> Messages { get; set; } = new();

    /// <summary>
    /// 
    /// </summary>
    public string? Source { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? Exception { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? ErrorId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? SupportMessage { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int StatusCode { get; set; }
}
