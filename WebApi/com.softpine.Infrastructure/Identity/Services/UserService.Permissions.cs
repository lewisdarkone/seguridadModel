using Microsoft.EntityFrameworkCore;
using com.softpine.muvany.core.Authorization;
using com.softpine.muvany.core.Exceptions;
using com.softpine.muvany.models.Constants;
using com.softpine.muvany.models.Tools;

namespace com.softpine.muvany.infrastructure.Identity.Services;

internal partial class UserService
{
    public async Task<List<string>> GetPermissionsAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userId);

        _ = user ?? throw new NotFoundException(ApiConstants.Messages.UserNotFound);

        var userRoles = await _userManager.GetRolesAsync(user);
        var permissions = new List<string>();
        foreach (var role in await _roleManager.Roles
            .Where(r => userRoles.Contains(r.Name))
            .ToListAsync(cancellationToken))
        {
            permissions.AddRange(await _db.RoleClaims
                .Where(rc => rc.RoleId == role.Id && rc.ClaimType == MuvanyClaims.Permission)
                .Select(rc => rc.ClaimValue)
                .ToListAsync(cancellationToken));
        }

        return permissions.Distinct().ToList();
    }

    //public async Task<bool> HasPermissionAsync(string userId, string permission, CancellationToken cancellationToken)
    //{
    //    var permissions = await _cache.GetOrSetAsync(
    //        _cacheKeys.GetCacheKey(MatricesClaims.Permission, userId),
    //        () => GetPermissionsAsync(userId, cancellationToken),
    //        cancellationToken: cancellationToken);

    //    return permissions?.Contains(permission) ?? false;
    //}

    public async Task<bool> HasPermissionAsync(string userId, string permission, CancellationToken cancellationToken)
    {
        //var permissions = await _cache.GetOrSetAsync(
        //    _cacheKeys.GetCacheKey(MatricesClaims.Permission, userId),
        //    () => GetPermissionsAsync(userId, cancellationToken),
        //    cancellationToken: cancellationToken);

        var permissions = await  GetPermissionsAsync(userId, cancellationToken);

        return permissions?.Where(x => x.Equals(Utils.ReplaceWhitespace(permission), StringComparison.OrdinalIgnoreCase)).ToList().Count()>0;
    }

    public Task InvalidatePermissionCacheAsync(string userId, CancellationToken cancellationToken) =>
        _cache.RemoveAsync(_cacheKeys.GetCacheKey(MuvanyClaims.Permission, userId), cancellationToken);
}
