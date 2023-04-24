using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.infrastructure.Identity.Entities;
using com.softpine.muvany.models.Interfaces;

namespace com.softpine.muvany.infrastructure.Identity.EFIdentityRepositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRolesClaimRepository : IRepositoryIdentity<ApplicationRoleClaim>, ITransientService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="permiso"></param>
        /// <returns></returns>
        Task<ApplicationRoleClaim> AddPermiso(ApplicationRoleClaim permiso);
    }
}
