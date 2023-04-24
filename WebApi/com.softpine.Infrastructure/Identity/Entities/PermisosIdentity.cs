namespace com.softpine.muvany.infrastructure.Identity.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PermisosIdentity
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int IdAccion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int IdRecurso { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? EsBasico { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<ApplicationRoleClaim> ApplicationRoleClaim { get; set; }
    }
}
