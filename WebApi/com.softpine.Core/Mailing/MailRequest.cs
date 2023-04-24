namespace com.softpine.muvany.core.Requests;

/// <summary>
/// 
/// </summary>
public class MailRequest
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="to"></param>
    /// <param name="subject"></param>
    /// <param name="body"></param>
    /// <param name="from"></param>
    /// <param name="displayName"></param>
    /// <param name="replyTo"></param>
    /// <param name="replyToName"></param>
    /// <param name="bcc"></param>
    /// <param name="cc"></param>
    /// <param name="attachmentData"></param>
    /// <param name="headers"></param>
    public MailRequest(List<string> to, string subject, string? body = null, string? from = null, string? displayName = null, string? replyTo = null, string? replyToName = null, List<string>? bcc = null, List<string>? cc = null, IDictionary<string, byte[]>? attachmentData = null, IDictionary<string, string>? headers = null)
    {
        To = to;
        Subject = subject;
        Body = body;
        From = from;
        DisplayName = displayName;
        ReplyTo = replyTo;
        ReplyToName = replyToName;
        Bcc = bcc ?? new List<string>();
        Cc = cc ?? new List<string>();
        AttachmentData = attachmentData ?? new Dictionary<string, byte[]>();
        Headers = headers ?? new Dictionary<string, string>();
    }

    /// <summary>
    /// 
    /// </summary>
    public List<string> To { get; }

    /// <summary>
    /// 
    /// </summary>
    public string Subject { get; }

    /// <summary>
    /// 
    /// </summary>
    public string? Body { get; }

    /// <summary>
    /// 
    /// </summary>
    public string? From { get; }

    /// <summary>
    /// 
    /// </summary>
    public string? DisplayName { get; }

    /// <summary>
    /// 
    /// </summary>
    public string? ReplyTo { get; }

    /// <summary>
    /// 
    /// </summary>
    public string? ReplyToName { get; }

    /// <summary>
    /// 
    /// </summary>
    public List<string> Bcc { get; }

    /// <summary>
    /// 
    /// </summary>
    public List<string> Cc { get; }

    /// <summary>
    /// 
    /// </summary>
    public IDictionary<string, byte[]> AttachmentData { get; }

    /// <summary>
    /// 
    /// </summary>
    public IDictionary<string, string> Headers { get; }
}
