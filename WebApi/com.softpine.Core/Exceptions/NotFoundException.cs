using System.Net;

namespace com.softpine.muvany.core.Exceptions;

/// <summary>
/// 
/// </summary>
public class NotFoundException : CustomException
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public NotFoundException(string message)
        : base(message, null, HttpStatusCode.NotFound)
    {
    }
}
