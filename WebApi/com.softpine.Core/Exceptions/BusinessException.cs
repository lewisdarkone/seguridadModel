namespace com.softpine.muvany.core.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public BusinessException()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public BusinessException(string message) : base(message)
        {

        }
    }
}
