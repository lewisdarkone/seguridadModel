using System.Net;

namespace com.softpine.muvany.core.Exceptions;

/// <summary>
/// 
/// </summary>
public class CustomException : Exception
{
    /// <summary>
    /// 
    /// </summary>
    public List<string>? ErrorMessages { get; }

    /// <summary>
    /// 
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="errors"></param>
    /// <param name="statusCode"></param>
    public CustomException(string message, List<string>? errors = default, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        ErrorMessages = errors;
        StatusCode = statusCode;
    }
}
