namespace com.softpine.muvany.models.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPasswordService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        string Hash(string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool Check(string hash, string password);
    }
}
