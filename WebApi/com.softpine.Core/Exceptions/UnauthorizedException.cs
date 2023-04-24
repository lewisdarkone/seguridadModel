using System.Net;

namespace com.softpine.muvany.core.Exceptions;

/// <summary>
/// 
/// </summary>
public class UnauthorizedException : CustomException
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public UnauthorizedException(string message)
       : base(message, null, HttpStatusCode.Unauthorized)
    {
    }
}
