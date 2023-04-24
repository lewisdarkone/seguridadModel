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
    public class PermisosRepository : BaseRepositoryIdentity<Permisos>, IPermisosRepository
    {
        private readonly IdentityContext _db;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public PermisosRepository(IdentityContext context) : base(context)
        {
            _db = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wildcard"></param>
        /// <returns></returns>
        public async Task<List<Permisos>> GetPermisos(string wildcard)
        {
            var permisos = await _db.Permisos
                .Include(x => x.IdAccionNavigation)
                .Include(x => x.IdRecursoNavigation).ToListAsync();

            if ( wildcard != null && wildcard.Count() > 2 )
            {
                permisos = permisos.Where(x => x.Descripcion.ToLower().Contains(wildcard.ToLower())).ToList();
                return permisos;
            }

            return permisos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permiso"></param>
        /// <returns></returns>
        public async Task<Permisos> AddPermiso(Permisos permiso)
        {
            await _entities.AddAsync(permiso);

            return permiso;
        }
    }
}
