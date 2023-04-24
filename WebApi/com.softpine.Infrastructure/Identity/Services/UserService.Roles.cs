using Microsoft.EntityFrameworkCore;
using com.softpine.muvany.core.Exceptions;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.Enumerations;
using com.softpine.muvany.models.Constants;

namespace com.softpine.muvany.infrastructure.Identity.Services;

internal partial class UserService
{
    public async Task<List<UserRoleDto>> GetRolesByUserIdAsync(string userId)
    {
        var userRoles = new List<UserRoleDto>();

        var user = await _userManager.FindByIdAsync(userId);

        var rolesUser = await _userManager.GetRolesAsync(user);

        foreach ( var item in rolesUser )
        {
            var roleUser = _roleManager.Roles.Where(c => c.TypeRol != null && c.Name == item.ToString()).FirstOrDefault();
            if ( roleUser != null )
                userRoles.Add(new UserRoleDto
                {
                    RoleId = roleUser.Id,
                    RoleName = roleUser.Name,
                    Description = roleUser.Description,
                    Enabled = _userManager.IsInRoleAsync(user, roleUser.Name).Result,
                    TypeRol = roleUser.TypeRol
                });
        }
        return userRoles;
    }

    public async Task<bool> AssignRolesAsync(string userId, UserRolesRequest request)
    {
        var userLoginId = _currentUser.GetUserId();
        var roleUserLogin = GetRolesByUserIdAsync(userLoginId).Result; //Usuario Login
        var roleUser = GetRolesByUserIdAsync(userId).Result;
        var isIntenoLogin = (int)ParametrosGeneralesEnum.Interno == roleUserLogin.FirstOrDefault().TypeRol ? true : false;//Usuario request
        var isIntenoUserId = (int)ParametrosGeneralesEnum.Interno == roleUser.FirstOrDefault().TypeRol ? true : false;

        var response = false;
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var user = await _userManager.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

        _ = user ?? throw new NotFoundException(ApiConstants.Messages.UserNotFound);

        foreach ( var userRole in request.UserRoles )
        {
            var foundRol = await _roleService.GetByIdAsync(userRole.RoleId);

            // Check if Role Exists
            if ( foundRol is not null )
            {
                var isInterRole = (int)ParametrosGeneralesEnum.Interno == foundRol.TypeRol ? true : false;
                if ( isIntenoUserId != isInterRole )
                    throw new BusinessException(ApiConstants.Messages.RoleNotAvailableForUser);

                if ( isIntenoLogin == false && isInterRole == true )
                    throw new BusinessException(ApiConstants.Messages.RoleNotAvailableForUser);
            }
        }

        foreach ( var userRole in request.UserRoles )
        {
            var foundRole = await _roleService.GetByIdAsync(userRole.RoleId);

            // Check if Role Exists
            if ( foundRole is not null )
            {
                var countExist = 0;
                foreach ( var roles in roleUser )
                {
                    if ( roles.RoleId == foundRole.Id )
                    {
                        countExist = 1;
                        break;
                    }
                }
                if ( countExist == 0 )
                    if ( !await _userManager.IsInRoleAsync(user, foundRole.Name) )
                    {
                        await _userManager.AddToRoleAsync(user, foundRole.Name);
                        response = true;
                    }
            }
        }

        foreach ( var roles in roleUser )
        {
            int vl = 0;
            foreach ( var userRole in request.UserRoles )
            {
                if ( roles.RoleId == userRole.RoleId )
                    break;
                vl++;
            }
            if ( vl == request.UserRoles.Count() )
                await _userManager.RemoveFromRoleAsync(user, roles.RoleName);
            response = true;
        }
        //await _events.PublishAsync(new ApplicationUserUpdatedEvent(user.Sucursal, true));

        return response;
    }
}
