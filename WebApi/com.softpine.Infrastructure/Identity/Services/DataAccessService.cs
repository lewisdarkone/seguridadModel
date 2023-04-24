using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.core.Services;
using com.softpine.muvany.infrastructure.Identity.Context;
using com.softpine.muvany.infrastructure.Identity.Entities;
using com.softpine.muvany.models.DTOS;

namespace com.softpine.muvany.infrastructure.Identity.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class IdentityDataAccessService : IIdentityDataAccessService
    {
        //private readonly IMemoryCache _cache;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IdentityContext _db;
        private readonly ICacheService _cache;
        private readonly ICacheKeyService _cacheKeys;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="signInManager"></param>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="db"></param>
        /// <param name="cache"></param>
        /// <param name="cacheKeys"></param>
        public IdentityDataAccessService(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IdentityContext db,
            ICacheService cache,
            ICacheKeyService cacheKeys)

        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
            _cache = cache;
            _cacheKeys = cacheKeys;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="httpVerb"></param>
        /// <returns></returns>
        public async Task <EndpointsPermisosDto> GetPermsisosEndpoint(ClaimsPrincipal principal, string controller, string action, string httpVerb)
        {
            var isAuthenticated = principal.Identity.IsAuthenticated;
            if ( !isAuthenticated )
            {
                return new EndpointsPermisosDto();
            }
           
            var endpoint = _db.Endpoints.Where(x => x.Controlador.ToUpper().Equals(controller.ToUpper()) 
                                                 && x.Metodo.ToUpper().Equals(action.ToUpper())
                                                 && x.HttpVerbo.ToUpper().Equals(httpVerb.ToUpper())
                                                 && x.Estado.Equals(true)).FirstOrDefault();        
            if ( endpoint != null )
            {
                var userId =  principal.GetUserId();
                var roleId = _db.UserRoles.Where(u => u.UserId == userId).FirstOrDefault().RoleId;
                var permisoEndpoint = _db.EndpointsPermisos.Where(x => x.EndpointId == endpoint.Id && x.Estado == true).FirstOrDefault();
                var permiso = _db.Permisos.Where(p => p.Id == permisoEndpoint.PermisoId).FirstOrDefault();
                var PermisoAccion = _db.Acciones.Where(x => x.Id == permiso.IdAccion && x.Estado.Equals(1)).FirstOrDefault().Nombre;
                var PermisoRecurso = _db.Recursos.Where(x => x.Id == permiso.IdRecurso && x.Estado.Equals(1)).FirstOrDefault().Nombre;
                var stringPermiso = $"Permisos.{PermisoRecurso}.{PermisoAccion}";
                var rolesClaims = _db.RoleClaims.Where(r => r.RoleId == roleId && r.ClaimType.ToLower() == "permisos" && r.ClaimValue.ToUpper() == stringPermiso.ToUpper()).FirstOrDefault();
                var data = new EndpointsPermisosDto();

                if (rolesClaims != null )
                {
                    return new EndpointsPermisosDto()
                    {
                        Id = permisoEndpoint.Id,
                        EndpointId = permisoEndpoint.EndpointId,
                        PermisoId = permisoEndpoint.PermisoId,
                        Estado = permisoEndpoint.Estado,
                        PermisoAccion = PermisoAccion,
                        PermisoRecurso = PermisoRecurso
                    };
                }               

               return data;
            }
            else
            {
                return new EndpointsPermisosDto();
            }
        }
    }
}
