namespace com.softpine.muvany.models.Interfaces.InterfacesJobs
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMatricesJobService : ITransientService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<bool> IsRunningGenerarFacturaTasacion();
    }
}
