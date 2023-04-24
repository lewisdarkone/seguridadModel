using System.Net;

namespace com.softpine.muvany.core.Exceptions;

/// <summary>
/// 
/// </summary>
public class ForbiddenException : CustomException
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public ForbiddenException(string message)
        : base(message, null, HttpStatusCode.Forbidden)
    {
    }
}
