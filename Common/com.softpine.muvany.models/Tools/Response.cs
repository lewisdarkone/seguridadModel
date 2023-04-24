namespace com.softpine.muvany.models.Tools
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T>
    {
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Data"></param>
        public Response(T Data)
        {
            Succeeded = true;
            Data = Data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="status"></param>
        public Response(string message, int status)
        {
            Succeeded = false;
            Message = message;
            Status = status;
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Succeeded { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string>? Errors { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public T? Data { get; set; }

    }
}
