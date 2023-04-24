namespace com.softpine.muvany.infrastructure.Identity.Options
{
    /// <summary>
    /// 
    /// </summary>
    public class PasswordOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public int SaltSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int KeySize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Iterations { get; set; }
    }
}
