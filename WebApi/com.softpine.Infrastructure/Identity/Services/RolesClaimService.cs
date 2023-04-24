using AutoMapper;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using com.softpine.muvany.core.Authorization;
using com.softpine.muvany.core.Events;
using com.softpine.muvany.core.Exceptions;
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.infrastructure.Identity.Context;
using com.softpine.muvany.infrastructure.Identity.Entities;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.QueryFilters;
using com.softpine.muvany.models.Entities.EntitiesIdentity;
using com.softpine.muvany.models.Constants;
using com.softpine.muvany.models.Request.RequestsIdentity;
using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.Tools;

namespace com.softpine.muvany.infrastructure.Identity.Services
{
    /// <summary>
    /// Servicio para los procesos para los mantenimientos de roles permisos
    /// </summary>
    public class RolesClaimService : IRolesClaimService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IdentityContext _db;
        private readonly ICurrentUser _currentUser;
        private readonly IEventPublisher _events;
        private readonly PaginationOptions _paginationOptions;
        /// <summary>
        /// 
        /// </summary>
        public readonly IMapper _mapper;
        /// <summary>
        /// Contructor para la inyeccion de las dependencias
        /// </summary>
        public readonly IPermisosService _ipermisosServices;
        /// <summary>
        /// Contructor para la inyección de recursos
        /// </summary>
        /// <param name="roleManager"></param>
        /// <param name="userManager"></param>
        /// <param name="db"></param>
        /// <param name="currentUser"></param>
        /// <param name="events"></param>
        /// <param name="mapper"></param>
        /// <param name="options"></param>
        /// <param name="ipermisosServices"></param>
        public RolesClaimService(
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IdentityContext db,
            ICurrentUser currentUser,
            IEventPublisher events,
            IMapper mapper,
            IPermisosService ipermisosServices
            , IOptions<PaginationOptions> options
            )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
            _currentUser = currentUser;
            _events = events;
            _mapper = mapper;
            _ipermisosServices = ipermisosServices;
            _paginationOptions = options.Value;
        }

        private async Task<RolesClaimDto> GetRolByIdAsync(string id) =>
           await _db.Roles.SingleOrDefaultAsync(x => x.Id == id) is { } role
          ? role.Adapt<RolesClaimDto>()
          : throw new NotFoundException("Role No Encontrado");

        /// <summary>
        /// Funcion que retorna todos los RolesClaims Registrados 
        /// </summary>
        public async Task<PagedList<ApplicationRoleClaimDto>> GetRolesClaimAsync(ApplicationRoleClaimQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var RoleClaims = _db.RoleClaims
                             .Join(_db.Permisos.Include(e => e.EndpointsPermisos), role => role.PermisoId, permiso => permiso.Id, (role, permiso) => new { role, permiso })
                             .Where(x => x.role.PermisoId != null && (x.permiso.EndpointsPermisos.Any() || x.permiso.IdAccion == 11 //[Tbl_Tas_Seg_Acciones] el 11 es VisualizarMenu
                             ) )
                             .Select(m => new ApplicationRoleClaimDto
                             {
                                 Id = m.role.Id,
                                 RoleId = m.role.RoleId,
                                 ClaimValue = m.role.ClaimValue,
                                 PermisoId = m.role.PermisoId,
                                 Permiso = new Permisos
                                 {
                                     Id = m.permiso.Id,
                                     Descripcion = m.permiso.Descripcion,
                                     IdAccion = m.permiso.IdAccion,
                                     IdRecurso = m.permiso.IdRecurso
                                 }
                             }).ToList();


            //await _db.RoleClaims.Include(r => r.PermisosNavigation)
            //             .ToListAsync();

            if ( RoleClaims == null || RoleClaims.Count() == 0 )
                throw new BusinessException(ApiConstants.Messages.DataEmptyError);

            if ( filters.Id != null && filters.Id != 0 )
                RoleClaims = RoleClaims.Where(p => p.Id == (int)filters.Id).ToList();

            if ( !string.IsNullOrEmpty(filters.ClaimValue) )
                RoleClaims = RoleClaims.Where(p => p.ClaimValue.Contains(filters.ClaimValue, StringComparison.OrdinalIgnoreCase)).ToList();

            if ( RoleClaims == null || RoleClaims.Count() == 0 )
                throw new BusinessException(ApiConstants.Messages.DataByfilterNotFound);

            var response = RoleClaims.Adapt<IEnumerable<ApplicationRoleClaimDto>>();
            var pagedPosts = PagedList<ApplicationRoleClaimDto>.Create(response, filters.PageNumber, filters.PageSize);
            return pagedPosts;
        }

        /// <summary>
        /// Servicio para Obtener los permisos de un Rol
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public async Task<RolesClaimDto> GetRolByIdWithPermissionsAsync(string roleId)
        {
            var role = await GetRolByIdAsync(roleId);
            var RoleClaims = await _db.RoleClaims
                             .Join(_db.Permisos.Include(e => e.EndpointsPermisos), role => role.PermisoId, permiso => permiso.Id, (role, permiso) => new { role, permiso })
                             .Select(m => new ApplicationRoleClaimDto
                             {
                                 Id = m.role.Id,
                                 RoleId = m.role.RoleId,
                                 ClaimValue = m.role.ClaimValue,
                                 PermisoId = m.role.PermisoId,
                                 ClaimType = m.role.ClaimType,
                                 Permiso = new Permisos
                                 {
                                     Id = m.permiso.Id,
                                     Descripcion = m.permiso.Descripcion,
                                     IdAccion = m.permiso.IdAccion,
                                     IdRecurso = m.permiso.IdRecurso,
                                     EndpointsPermisos = m.permiso.EndpointsPermisos.Where(p=> p.Endpoint.Estado ).ToList()
                                 }
                             })
                              .Where(c => c.RoleId == roleId && c.PermisoId != null && c.ClaimType == MuvanyClaims.Permission)
                              .ToListAsync();

            var dataPermissions = new List<PermissionsRequest>();
            foreach ( var item in RoleClaims )
            {
                //var splitClaim = item.ClaimValue.ToString().Split(".");

                if ( !item.Permiso.EndpointsPermisos.Any() && item.Permiso.IdAccion != 11 )
                {
                    continue;
                }

                var responsePermissions = new PermissionsRequest { Id = item.Permiso.Id, Descripcion = item.Permiso.Descripcion };
                // var responsePermissions = await _ipermisosServices.GetPermisosClaims(splitClaim[2].ToString(), splitClaim[1].ToString());
                //if ( responsePermissions == null )
                //    throw new CustomException("No hay permisos asociados a esta descripción");
                dataPermissions.Add(responsePermissions);

            }
            role.Permisos = dataPermissions;
            return role;
        }

        /// <summary>
        /// Funcion para agregar los permisos de un rol
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="InternalServerException"></exception>
        public async Task<bool> AddPermissionsRolAsync(CreateRolePermissionsRequest request)
        {
            var result = false;
            var role = await _roleManager.FindByIdAsync(request.RoleId);
            _ = role ?? throw new NotFoundException("Role No Encontrado");

            var dataR = IsExistPermission(request.Permissions);

            var currentClaims = await _roleManager.GetClaimsAsync(role);

            // Verifing that the permission is not assign to the rol
            var isAssing = 0;
            foreach ( var claim in currentClaims.Where(c => dataR.Any(p => p.Description == c.Value)) )
            {
                isAssing++;
            }
            if ( isAssing == dataR.Count() )
                throw new InternalServerException($"Todos los Permisos estan asignados al rol seleccionado, posteriormente.");

            // Add all permissions that were not previously selected
            foreach ( GenericSelectList permission in dataR.Where(c => !currentClaims.Any(p => p.Value == c.Description)) )
            {
                if ( !string.IsNullOrEmpty(permission.Description) )
                {
                    _db.RoleClaims.Add(new ApplicationRoleClaim
                    {
                        RoleId = role.Id,
                        ClaimType = MuvanyClaims.Permission,
                        ClaimValue = permission.Description,
                        PermisoId = (int)permission.Value,
                    });

                    var response = await _db.SaveChangesAsync();
                    result = response > 0;
                }
            }

            return result;
        }

        /// <summary>
        /// Funcion para actualizar los permisos de un rol
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="InternalServerException"></exception>
        public async Task<bool> UpdatePermissionsRolAsync(UpdateRolePermissionsRequest request)
        {
            var result = false;
            var role = await _roleManager.FindByIdAsync(request.RoleId);
            _ = role ?? throw new NotFoundException("Role No Encontrado");

            var dataR = IsExistPermission(request.Permissions);

            var currentClaims = await _roleManager.GetClaimsAsync(role);

            // Remove permissions that were previously selected

            foreach ( var permission in currentClaims.Where(c => !dataR.Any(p => p.Description.ToUpper() == c.Value.ToString().ToUpper())) )
            {
                if ( !string.IsNullOrEmpty(permission.Value) )
                {
                    var removeResult = await _roleManager.RemoveClaimAsync(role, permission);
                    if ( !removeResult.Succeeded )
                    {
                        throw new InternalServerException($"Error eliminando el permiso {permission} del rol");
                    }
                }
            }

            foreach ( var permission in dataR.Where(c => !currentClaims.Any(p => p.Value.ToUpper() == c.Description.ToUpper())) )
            {
                if ( !string.IsNullOrEmpty(permission.Description) )
                {
                    _db.RoleClaims.Add(new ApplicationRoleClaim
                    {
                        RoleId = role.Id,
                        ClaimType = MuvanyClaims.Permission,
                        ClaimValue = permission.Description.ToLower(),
                        PermisoId = (int)permission.Value,
                    });

                    var response = await _db.SaveChangesAsync();
                    result = response > 0;
                }
            }
            result = true;


            //await _events.PublishAsync(new ApplicationRoleUpdatedEvent(role.Sucursal, role.Name, true));

            return result;
        }
        private List<GenericSelectList> IsExistPermission(List<PermissionsRequest> permissions)
        {
            var result = new List<GenericSelectList>();

            foreach ( var row in permissions )
            {
                var valData = _db.Permisos
                    .Include(e => e.EndpointsPermisos)
                    .Include(p => p.IdRecursoNavigation)
                    .Include(p => p.IdAccionNavigation)
                    .Where(r => r.Id == row.Id)
                    .FirstOrDefaultAsync().Result;
                if ( valData == null )
                    throw new NotFoundException($"El permiso:  {row.Id},  No existe ó no esta agregado a ningún endpoint, favor verificar los datos.");
                var valAccion = valData.IdAccionNavigation.Nombre;
                var valRecurso = valData.IdRecursoNavigation.Nombre;
                var permiso = new GenericSelectList();
                permiso.Description = $"{MuvanyClaims.Permission}.{valRecurso}.{valAccion}";
                permiso.Value = row.Id;
                result.Add(permiso);
            }

            return result;
        }

        /// <summary>
        /// Funcion para eliminar los permisos de un rol
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="NotContentException"></exception>
        /// <exception cref="InternalServerException"></exception>
        public async Task<bool> DeletePermissionsRolAsync(DeleteRolePermissionsRequest request)
        {
            var result = false;
            var role = await _roleManager.FindByIdAsync(request.RoleId);
            _ = role ?? throw new NotFoundException("Rol no encontrado");

            var dataR = IsExistPermission(request.Permisos);
            if ( dataR == null )
                throw new NotContentException("No existen estos permisos cargados a este Rol");
            var currentClaims = await _roleManager.GetClaimsAsync(role);

            // Remove permissions that were previously selected
            foreach ( var claim in currentClaims.Where(c => dataR.Any(p => p.Description.ToUpper() == c.Value.ToUpper())) )
            {
                result = false;
                var removeResult = await _roleManager.RemoveClaimAsync(role, claim);
                if ( !removeResult.Succeeded )
                {
                    throw new InternalServerException("La Actulización de los permisos falló.");
                }
                result = true;
            }

            return result;
        }
    }
}
