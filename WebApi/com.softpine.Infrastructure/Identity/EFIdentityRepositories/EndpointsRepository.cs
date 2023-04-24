using Microsoft.EntityFrameworkCore;
using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.infrastructure.Identity.Context;
using com.softpine.muvany.infrastructure.Identity.EFIdentityRepositories;
using com.softpine.muvany.core.Exceptions;
using com.softpine.muvany.models.Entities.EntitiesIdentity;
using com.softpine.muvany.models.Constants;

namespace com.softpine.muvany.infrastructure.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class EndpointsRepository : BaseRepositoryIdentity<Endpoints>, IEndpointsRepository
    {
        private readonly IdentityContext _db;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public EndpointsRepository(IdentityContext context) : base(context) {
            _db = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public async Task<Endpoints> AddEndpoint(Endpoints endpoint)
        {
            await _entities.AddAsync(endpoint);

            return endpoint;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Endpoints>> GetAllEndpoints()
        {
            return _db.Endpoints
                .Include(x => x.EndpointsPermisos)
                .ThenInclude(v => v.Permiso).ThenInclude(x => x.IdAccionNavigation)
                .Include(x => x.EndpointsPermisos)
                .ThenInclude(v => v.Permiso).ThenInclude(x => x.IdRecursoNavigation).ToList();     
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="httpVerb"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<Endpoints> GetActiveEndpoint(string controller, string action, string httpVerb)
        {
            var endpoint = _db.Endpoints
                .Include(x => x.EndpointsPermisos)
                .ThenInclude(v => v.Permiso).ThenInclude(x => x.IdAccionNavigation)
                .Include(x => x.EndpointsPermisos)
                .ThenInclude(v => v.Permiso).ThenInclude(x => x.IdRecursoNavigation)
                                .Where(x => x.Controlador.ToUpper().Equals(controller.ToUpper())
                                && x.Metodo.ToUpper().Equals(action.ToUpper())
                                && x.HttpVerbo.ToUpper().Equals(httpVerb.ToUpper())
                                && x.Estado.Equals(true))
                               .FirstOrDefault();

            if ( endpoint == null )
            {
                throw new NotFoundException(ApiConstants.Messages.EndpointNotFound);
            }
            
            return endpoint;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<Endpoints> GetActiveEndpointPermisos(string controller, string action)
        {
            var endpoint = _db.Endpoints
                .Include(x => x.EndpointsPermisos)
                .ThenInclude(v => v.Permiso).ThenInclude(x => x.IdAccionNavigation)
                .Include(x => x.EndpointsPermisos)
                .ThenInclude(v => v.Permiso).ThenInclude(x => x.IdRecursoNavigation)
                                .Where(x => x.EndpointsPermisos.Permiso.IdRecursoNavigation.Nombre.ToUpper().Equals(controller.ToUpper())
                                && x.EndpointsPermisos.Permiso.IdAccionNavigation.Nombre.ToUpper().Equals(action.ToUpper())
                                && x.Estado.Equals(true))
                               .FirstOrDefault();

            if (endpoint == null)
            {
                throw new NotFoundException($"The endpoints recurso:{controller}, acción:{action} does not exist");
            }

            return endpoint;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Endpoints> GetEndpointById(int id)
        {
            var endpoint = await _db.Endpoints.Include(x => x.EndpointsPermisos).Where(v => v.Id == id).FirstOrDefaultAsync();

            return endpoint;
        }

    }
}
