using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using com.softpine.muvany.core.Authorization;
using com.softpine.muvany.core.Events;
using com.softpine.muvany.core.Exceptions;
using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.infrastructure.Identity.Context;
using com.softpine.muvany.infrastructure.Identity.Entities;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.QueryFilters;
using com.softpine.muvany.models.Constants;
using com.softpine.muvany.models.Requests;

namespace Template.WebApi.Infrastructure.Identity;
/// <summary>
/// Servicio para los procesos relacionados a los roles 
/// </summary>
internal class RoleService : IRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IdentityContext _db;
    private readonly ICurrentUser _currentUser;
    private readonly IEventPublisher _events;
    private readonly PaginationOptions _paginationOptions;
    private readonly IMapper _mapper;

    /// <summary>
    /// Contructor para la inyeccion de las dependencias
    /// </summary>
    /// <param name="roleManager"></param>
    /// <param name="userManager"></param>
    /// <param name="db"></param>
    /// <param name="options"></param>
    /// <param name="currentUser"></param>
    /// <param name="events"></param>
    /// <param name="mapper"></param>
    public RoleService(
        RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager,
        IdentityContext db,
        IOptions<PaginationOptions> options,
        ICurrentUser currentUser,
        IEventPublisher events
        , IMapper mapper)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _db = db;
        _paginationOptions = options.Value;
        _currentUser = currentUser;
        _events = events;
        _mapper = mapper;

    }

    public async Task<PagedList<RoleDto>> GetListAsync(RolesQueryFilter filters)
    {
        filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
        filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
        var roles = _roleManager.Roles.Where(r => r.TypeRol != null).ToListAsync().Result;
        if ( roles == null || roles.Count() == 0 )
            throw new BusinessException(ApiConstants.Messages.DataEmptyError);
        if ( !string.IsNullOrEmpty(filters.Id) )
            roles = roles.Where(p => p.Id == filters.Id).ToList();
        if ( filters.TypeRol != null && filters.TypeRol != 0 )
            roles = roles.Where(p => p.TypeRol == filters.TypeRol).ToList();

        if ( !string.IsNullOrEmpty(filters.Nombre) )
            roles = roles.Where(p => p.Name.Contains(filters.Nombre, StringComparison.OrdinalIgnoreCase)).ToList();

        //var response = roles.Adapt<List<RoleDto>>();
        var response = _mapper.Map<IEnumerable<RoleDto>>(roles);
        var pagedPosts = PagedList<RoleDto>.Create(response, filters.PageNumber, filters.PageSize);
        return pagedPosts;

    }

    public async Task<int> GetCountAsync() =>
        await _roleManager.Roles.CountAsync();

    public async Task<bool> ExistsAsync(string roleName, string? excludeId) =>
        await _roleManager.FindByNameAsync(roleName)
            is ApplicationRole existingRole
            && existingRole.Id != excludeId;

    public async Task<RoleDto> GetByIdAsync(string id)
    {
        var response = await _db.Roles.Where(r => r.TypeRol != null).SingleOrDefaultAsync(x => x.Id == id);
        if ( response == null || response.Id == "" )
            throw new NotFoundException(ApiConstants.Messages.RoleNotFound);

        return _mapper.Map<RoleDto>(response);
    }

    public async Task<bool> CreateAsync(CreateRoleRequest request)
    {
        // Create a new role.
        var role = new ApplicationRole(request.Name, request.Description, request.TypeRol);
        var result = await _roleManager.CreateAsync(role);

        if ( !result.Succeeded )
        {
            throw new InternalServerException(ApiConstants.Messages.CreationFailed);
        }

        //await _events.PublishAsync(new ApplicationRoleCreatedEvent(role.Sucursal, role.Name));

        return result.Succeeded;

    }

    public async Task<bool> UpdateAsync(UpdateRoleRequest request)
    {

        // Update an existing role.
        var role = await _roleManager.FindByIdAsync(request.Id);

        _ = role ?? throw new NotFoundException(ApiConstants.Messages.RoleNotFound);

        if ( MuvanyRoles.IsDefault(role.Name) )
        {
            throw new ConflictException(ApiConstants.Messages.UpdateNotAllowed);
        }

        role.Name = request.Name;
        role.NormalizedName = request.Name.ToUpperInvariant();
        role.Description = request.Description;
        var result = await _roleManager.UpdateAsync(role);

        if ( !result.Succeeded )
        {
            throw new InternalServerException(ApiConstants.Messages.UpdateFailed);
        }

        //await _events.PublishAsync(new ApplicationRoleUpdatedEvent(role.Sucursal, role.Name));

        return result.Succeeded;

    }

    public async Task<bool> UpdateRolesStatusAsync(ToggleRoleStatusRequest request)
    {

        // Update an existing role.
        var role = await _roleManager.FindByIdAsync(request.Id);

        _ = role ?? throw new NotFoundException("Role Not Found");

        var result = await _roleManager.Roles.Where(r => r.Id == request.Id).ToListAsync();

        //if (!result.Succeeded)
        //{
        //    throw new InternalServerException("Update role failed");
        //}

        //await _events.PublishAsync(new ApplicationRoleUpdatedEvent(role.Sucursal, role.Name));

        return true;

    }

    public async Task<bool> DeleteAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);

        _ = role ?? throw new NotFoundException(ApiConstants.Messages.RoleNotFound);

        if ( MuvanyRoles.IsDefault(role.Name) )
        {
            throw new ConflictException(ApiConstants.Messages.DeletionNotAllowed);
        }

        if ( (await _userManager.GetUsersInRoleAsync(role.Name)).Count > 0 )
        {
            throw new ConflictException(ApiConstants.Messages.DeletionNotAllowed);
        }

        var result = await _roleManager.DeleteAsync(role);
        if ( !result.Succeeded )
        {
            throw new BusinessException(ApiConstants.Messages.DeletionNotAllowed);
        }
        //await _events.PublishAsync(new ApplicationRoleDeletedEvent(role.Sucursal, role.Name));

        return result.Succeeded;
    }
}
