namespace com.softpine.muvany.models.CustomEntities
{
    /// <summary>
    /// 
    /// </summary>
    public class Metadata
    {
        /// <summary>
        /// 
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool HasNextPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool HasPreviousPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? NextPageUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? PreviousPageUrl { get; set; }
    }
}
