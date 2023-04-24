namespace com.softpine.muvany.models.QueryFilters
{
    /// <summary>
    /// Clase para filtrar los endpoint registrados
    /// </summary>
    public class EndpointsQueryFilter : BasePostQueryFilter
    {
        /// <summary>
        /// 
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Nombre { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Controlador { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Metodo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? HttpVerbo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? IsActive { get; set; }
    }
}
