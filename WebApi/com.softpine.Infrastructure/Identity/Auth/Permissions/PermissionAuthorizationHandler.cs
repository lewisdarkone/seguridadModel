using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using com.softpine.muvany.core.Authorization;
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.infrastructure.Identity.Services;

namespace com.softpine.muvany.infrastructure.Identity.Auth.Permissions;

internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IUserService _userService;
    private readonly IIdentityDataAccessService _dataService;
    private readonly IPermisosService _permisosService;

    public PermissionAuthorizationHandler(IUserService userService, IPermisosService permisosService)
    {
        _userService = userService;
        _permisosService = permisosService;
    }
      
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if ( requirement.Permission.Equals("Authorization") )
        {     
            var routeValues = (context.Resource as HttpContext).Request.RouteValues;
            var _httpVerb = (context.Resource as HttpContext).Request.Method;
            if ( routeValues != null )
            {
                var _controller = routeValues["controller"].ToString();
                var _action = routeValues["action"].ToString();

                if ( context.User.Identity.IsAuthenticated && _controller != null && _action != null )
                {
                    var permiso = await _permisosService.GetPermisoEndpoint(_controller.ToString(), _action.ToString(), _httpVerb.ToString()); 

                    if ( permiso != null && permiso.Id > 0)
                    {
                        var stringPermiso = $"{MuvanyClaims.Permission}.{permiso.RecursoNombre}.{permiso.AccionNombre}";

                        if ( context.User?.GetUserId() is { } userId &&
                                await _userService.HasPermissionAsync(userId, stringPermiso) )
                        {
                            context.Succeed(requirement);
                        }
                    }
                }
            }
        }
    }
}
