using com.softpine.muvany.infrastructure.Identity.Context;
using com.softpine.muvany.infrastructure.Identity.Entities;

namespace com.softpine.muvany.infrastructure.Identity.EFIdentityRepositories
{
    /// <summary>
    /// Repositorio para los procesos relacionados a los permisos que se asignan a los roles
    /// </summary>
    public class RolesClaimRepository : BaseRepositoryIdentity<ApplicationRoleClaim>, IRolesClaimRepository
    {
        private readonly IdentityContext _context;
        /// <summary>
        /// Contructor para inyectar los componentes
        /// </summary>
        /// <param name="context"></param>
        public RolesClaimRepository(IdentityContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permiso"></param>
        /// <returns></returns>
        public async Task<ApplicationRoleClaim> AddPermiso(ApplicationRoleClaim permiso)
        {
            await _entities.AddAsync(permiso);

            return permiso;
        }
    }
}
