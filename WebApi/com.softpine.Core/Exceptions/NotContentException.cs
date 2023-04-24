using System.Net;

namespace com.softpine.muvany.core.Exceptions;
/// <summary>
/// 
/// </summary>
public class NotContentException : CustomException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errors"></param>
        public NotContentException(string message, List<string>? errors = default)
            : base(message, errors, HttpStatusCode.NoContent)
        {
        }
    }

