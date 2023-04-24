using System.Net;

namespace com.softpine.muvany.core.Exceptions;

/// <summary>
/// 
/// </summary>
public class ConflictException : CustomException
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public ConflictException(string message)
        : base(message, null, HttpStatusCode.Conflict)
    {
    }
}
