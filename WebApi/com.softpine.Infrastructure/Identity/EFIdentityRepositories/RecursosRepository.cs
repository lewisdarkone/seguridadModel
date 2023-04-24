using Microsoft.EntityFrameworkCore;
using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.infrastructure.Identity.Context;
using com.softpine.muvany.infrastructure.Identity.EFIdentityRepositories;
using com.softpine.muvany.models.Entities.EntitiesIdentity;

namespace com.softpine.muvany.infrastructure.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class RecursosRepository: BaseRepositoryIdentity<Recursos>, IRecursosRepository
    {
        private readonly IdentityContext _db;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public RecursosRepository(IdentityContext context) : base(context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Recursos>> GetAllRecursos()
        {
            var recursos = _entities.Include(p => p.IdModuloNavigation);

            return recursos;
        }
    }
}
