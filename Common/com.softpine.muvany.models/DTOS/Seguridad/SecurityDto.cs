

using com.softpine.muvany.models.Enumerations;

namespace com.softpine.muvany.models.DTOS
{
    /// <summary>
    /// 
    /// </summary>
    public class SeguridadDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public RoleType? Role { get; set; }
    }
}
