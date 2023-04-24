namespace com.softpine.muvany.models.QueryFilters
{
    /// <summary>
    /// 
    /// </summary>
    public class UserQueryFilter : BasePostQueryFilter
    {
        /// <summary>
        /// 
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? NombreCompleto { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? Activo { get; set; }

    }
}
