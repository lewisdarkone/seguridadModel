using System.Text.RegularExpressions;
using Ardalis.Specification.EntityFrameworkCore;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using com.softpine.muvany.core.Events;
using com.softpine.muvany.core.Exceptions;
using com.softpine.muvany.core.Identity.InterfacesIdentity;
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.infrastructure.Identity.Auth;
using com.softpine.muvany.infrastructure.Identity.Context;
using com.softpine.muvany.infrastructure.Identity.Entities;
using com.softpine.muvany.core.Authorization;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.core.Services;
using com.softpine.muvany.models.CustomEntities;
using com.softpine.muvany.models.Tools;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.Specification;
using com.softpine.muvany.models.QueryFilters;
using com.softpine.muvany.models.Constants;
using com.softpine.muvany.models.Enumerations;
using Microsoft.Extensions.Logging;

namespace com.softpine.muvany.infrastructure.Identity.Services;

internal partial class UserService : IUserService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IdentityContext _db;
    private readonly IJobService _jobService;
    private readonly IMailService _mailService;
    private readonly SecuritySettings _securitySettings;
    private readonly IEmailMatricesService _templateService;
    private readonly IFileStorageService _fileStorage;
    private readonly IEventPublisher _events;
    private readonly ICacheService _cache;
    private readonly ICacheKeyService _cacheKeys;
    private readonly PaginationOptions _paginationOptions;
    private readonly ICurrentUser _currentUser;
    private readonly ConfigurationsConstants _configurationsConstants;
    private readonly IRoleService _roleService;
    private readonly IUsersDomainService _usersDomainService;


    public UserService(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IdentityContext db,
        IOptions<PaginationOptions> paginationOptions,
        IOptions<SecuritySettings> securitySettings
        , ICurrentUser currentUser
        , IOptions<ConfigurationsConstants> configurationsConstants
        , IRoleService roleService
        , IUsersDomainService usersDomainService
        , IMailService mailService
        , IEventPublisher events

        )
    {
        _events = events;
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _db = db;
        _securitySettings = securitySettings.Value;
        _paginationOptions = paginationOptions.Value;
        _currentUser = currentUser;
        _configurationsConstants = configurationsConstants.Value;
        _roleService = roleService;
        _mailService = mailService;
        _usersDomainService = usersDomainService;

    }

    public async Task<PaginationResponse<UserDetailsDto>> SearchAsync(UserListFilter filter, CancellationToken cancellationToken)
    {
        var spec = new EntitiesByPaginationFilterSpec<ApplicationUser>(filter);

        var users = await _userManager.Users
            .WithSpecification(spec)
            .ProjectToType<UserDetailsDto>()
            .ToListAsync(cancellationToken);
        int count = await _userManager.Users
            .CountAsync(cancellationToken);

        return new PaginationResponse<UserDetailsDto>(users, count, filter.PageNumber, filter.PageSize);
    }

    public async Task<bool> ExistsWithNameAsync(string name)
    {

        return await _userManager.FindByNameAsync(name) is not null;
    }

    /// <summary>
    /// Validar un código de valicacion enviado
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="validationCode"></param>
    /// <returns></returns>
    public async Task<bool> ValidateCodeIsValid(string userId, string validationCode)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if ( user == null )
            throw new NotFoundException(ApiConstants.Messages.UserNotFound);

        if ( validationCode != user.TempCode)
            throw new BusinessException(ApiConstants.Messages.ValidationCodeIncorrect);

        if ( user.ExpireTempCodeDate < DateTime.Now )
            throw new BusinessException(ApiConstants.Messages.ValidationCodeExpired);

        return true;
    }
    public async Task<bool> IsPhoneNumber(string? number)
    {
        var response = false;
        if ( number is not null )
        {
            var cleaned = RemoveNonNumeric(number);

            if ( cleaned.Length == 10 )
                return true;
            else
                return false;
        }
        await Task.CompletedTask.WaitAsync(TimeSpan.FromSeconds(0));
        return response;
    }

    public static string RemoveNonNumeric(string phone)
    {
        return Regex.Replace(phone, @"[^0-9]+", "");
    }
    public async Task<bool> ExistsWithEmailAsync(string email, string? exceptId = null)
    {

        return await _userManager.FindByEmailAsync(email.Normalize()) is ApplicationUser user && user.Id != exceptId;
    }

    public async Task<bool> ExistsWithPhoneNumberAsync(string phoneNumber, string? exceptId = null, string email = "", string role = "")
    {
        var result = await _userManager.Users.Where(x => x.PhoneNumber == phoneNumber && x.Estado == 0).ToListAsync();
        if ( result == null || result.Count == 0 )
            return false;
        if ( result.Where(f => f.Email.Contains(_configurationsConstants.EmailInternoFirma)).Count() > 0 )
            return false;
        return true;
        // return await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber && !x.Email.Contains(_configurationsConstants.EmailInternoFirma) && x.Estado == 0) is ApplicationUser user && user.Sucursal != exceptId;
    }

    public async Task<PagedList<UserDetailsDto>> GetListAsync(UserQueryFilter filter)
    {
        var valUsers = new List<UserDetailsDto>();
        var userLoginId = _currentUser.GetUserId();

        foreach ( var p in _userManager.Users )
        {
            var nombreSucursal = "";
            var departamento = ""; 

            valUsers.Add(new UserDetailsDto()
            {
                Id = Guid.Parse(p.Id),
                UserName = p.UserName,
                PhoneNumber = p.PhoneNumber,
                Email = p.Email,
                IsActive = (p.Estado == 1 ? true : false),
                NombreCompleto = p.NombreCompleto
            });
        }

        foreach ( var userF in valUsers )
        {


            userF.Roles = GetRolesByUserIdAsync(userF.Id.ToString()).Result;
        }


        var response = valUsers;
        if ( response.Count == 0 )
        { throw new NotContentException(ApiConstants.Messages.DataEmptyError); }

        if ( !string.IsNullOrEmpty(filter.Email) )
        {
            response = response.Where(p => p.Email.Equals(filter.Email)).ToList();
        }

        if ( !string.IsNullOrEmpty(filter.NombreCompleto) )
        {
            response = response.Where(p => (p.NombreCompleto).ToUpper().Contains(filter.NombreCompleto.ToUpper())).ToList();
        }

        if ( filter.Activo != null )
        {
            response = response.Where(p => p.IsActive.ToString().ToUpper().Equals(filter.Activo.ToString().ToUpper())).ToList();
        }        
        

        if ( filter.PageNumber == 0 || filter.PageSize == 0 )
        {
            filter.PageNumber = _paginationOptions.DefaultPageNumber;
            filter.PageSize = _paginationOptions.DefaultPageSize;
        }

        var result = PagedList<UserDetailsDto>.Create(response, filter.PageNumber, filter.PageSize);
        return result;
    }

    /// <summary>
    /// Obtener todos los usuarios sin restrinciones
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public async Task<IEnumerable<UserDetailsDto>> GetUsersAllAsync(UserQueryFilter filter)
    {
        var valUsers = new List<UserDetailsDto>();
        var userLoginId = _currentUser.GetUserId();

        valUsers =
                await _userManager.Users.Select(p =>
                new UserDetailsDto
                {
                    Id = Guid.Parse(p.Id),
                    UserName = p.UserName,
                    PhoneNumber = p.PhoneNumber,
                    Email = p.Email,
                    IsActive = (p.Estado == 0 ? true : false),
                    NombreCompleto = p.NombreCompleto
                })
                .AsNoTracking()
                .ToListAsync();

        var response = valUsers;
        if ( response.Count == 0 )
        { throw new NotContentException(ApiConstants.Messages.DataEmptyError); }

        if ( !string.IsNullOrEmpty(filter.Email) )
        {
            response = response.Where(p => p.Email.Equals(filter.Email)).ToList();
        }

        if ( !string.IsNullOrEmpty(filter.NombreCompleto) )
        {
            response = response.Where(p => (p.NombreCompleto).ToUpper().Contains(filter.NombreCompleto.ToUpper())).ToList();
        }

        if ( filter.Activo != null )
        {
            response = response.Where(p => p.IsActive.ToString().ToUpper().Equals(filter.Activo.ToString().ToUpper())).ToList();
        }

        if ( filter.PageNumber == 0 || filter.PageSize == 0 )
        {
            filter.PageNumber = _paginationOptions.DefaultPageNumber;
            filter.PageSize = _paginationOptions.DefaultPageSize;
        }


        return response;
    }


    public async Task<UserDetailsDto> GetUserWithoutSupplierAsync(string userId)
    {
        var valUser = new UserDetailsDto();

        var roles = await GetRolesByUserIdAsync(userId);
        var vResponse = from u in _userManager.Users

                        where u.Id == userId
                        select new UserDetailsDto
                        {
                            Id = Guid.Parse(u.Id),
                            UserName = u.UserName,
                            PhoneNumber = u.PhoneNumber,
                            Email = u.Email,
                            IsActive = (u.Estado == 0 ? true : false),
                            EmailConfirmed = u.EmailConfirmed,
                            NombreCompleto = u.NombreCompleto,
                            Roles = roles,
                            TwoFactorEnable = u.TwoFactorEnabled
                        };

        if ( vResponse == null || vResponse.ToList().Count() < 1 )
            throw new BusinessException(ApiConstants.Messages.UserNotFound);

        valUser = await vResponse.FirstOrDefaultAsync();
        return valUser;
    }

    public Task<int> GetCountAsync(CancellationToken cancellationToken) =>
        _userManager.Users.AsNoTracking().CountAsync(cancellationToken);

    public async Task<UserDetailsDto> GetUserByIdAsync(string userId)
    {
        var _user = await _userManager.FindByIdAsync(userId);
        _ = _user ?? throw new NotFoundException(ApiConstants.Messages.UserNotFound);

        return await GetUserWithoutSupplierAsync(userId);

    }

    public async Task<UserDetailsDto> GetUserByUserNameAsync(string userName)
    {
        var _user = await _userManager.FindByNameAsync(userName);
        _ = _user ?? throw new NotFoundException(ApiConstants.Messages.UserNotFound);

        return await GetUserWithoutSupplierAsync(_user.Id);


    }



    public async Task ToggleStatusAsync(ToggleUserStatusRequest request)
    {
        var user = await _userManager.Users.Where(u => u.Id == request.UserId).FirstOrDefaultAsync();

        _ = user ?? throw new NotFoundException(ApiConstants.Messages.UserNotFound);

        bool isAdmin = await _userManager.IsInRoleAsync(user, MuvanyRoles.Administrador);
        if ( isAdmin )
        {
            throw new ConflictException(ApiConstants.Messages.UpdateNotAllowed);
        }

        user.Estado = request.ActivateUser == true ? 1 : 0;

        await _userManager.UpdateAsync(user);

        await _events.PublishAsync(new ApplicationUserUpdatedEvent(user.Id));
    }

    public async Task<bool> ValidateUserInterAsync(string userId)
    {
        var roleUser = await GetRolesByUserIdAsync(userId);

        if ( roleUser == null )
        {
            throw new BusinessException(ApiConstants.Messages.RoleNotFound);
        }

        return (int)ParametrosGeneralesEnum.Interno == roleUser.FirstOrDefault().TypeRol ? true : false;
    }

    private async Task<bool> ValidateRolInterAsync(string RoleId)
    {
        var response = false;
        var roleUser = _roleService.GetByIdAsync(RoleId).Result;

        if ( roleUser == null )
        {
            return response = true;
        }

        return (int)ParametrosGeneralesEnum.Interno == roleUser.TypeRol ? true : false;
    }
    public async Task<bool> ChangeStatusAsync(ToggleUserStatusRequest request)
    {
        bool result = false;
        var userLoginId = _currentUser.GetUserId();
        if ( request.UserId == userLoginId )
            throw new NotFoundException(ApiConstants.Messages.NotDisableYourUser);



        var user = await _userManager.Users.Where(u => u.Id == request.UserId).FirstOrDefaultAsync();

        if ( user == null || user.Id == null )
            throw new NotFoundException(ApiConstants.Messages.UserNotFound);

        user.Estado = request.ActivateUser == true ? 1 : 0;

        var response = await _userManager.UpdateAsync(user);
        if ( response.Succeeded )
        {
            result = true;
        }
        return result;
    }


    public async Task<string> GetUserLoginId() => _currentUser.GetUserId();
    public async Task<UserDetailsDto> GetUserLoginDetails()
    {
        string idUser = await GetUserLoginId();
        var result = new ApplicationUser();

        result = await _userManager.Users
        .AsNoTracking()
        .Where(u => u.Id == idUser)
        .FirstOrDefaultAsync();

        _ = result ?? throw new NotFoundException(ApiConstants.Messages.UserNotFound);
        return result.Adapt<UserDetailsDto>();

    }

    public async Task<bool> AddFailLoggin(string userId)
    {
        bool result = false;

        var user = await _userManager.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

        if (user == null || user.Id == null)
            throw new NotFoundException(ApiConstants.Messages.UserNotFound);

        user.AccessFailedCount += 1;

        if (user.AccessFailedCount > (int)_configurationsConstants.RetryIncorrectPasswordLimit) 
        {
            user.Estado = 0;
            user.AccessFailedCount = 0;
        } 

        var response = await _userManager.UpdateAsync(user);
        if (response.Succeeded)
        {
            result = true;
        }
        return result;
    }

    /// <summary>
    /// Registrar un usuario nuevo via app
    /// </summary>
    /// <param name="request"></param>
    /// <param name="origin"></param>
    /// <returns></returns>
    /// <exception cref="BusinessException"></exception>
    public async Task<bool> RegisterUserAsync(RegisterUserRequest request, string origin)
    {
        bool result;
        var userFilter = new UserQueryFilter();
        userFilter.Email = request.Email;

        var findUser = await GetListAsync(userFilter);
        if (findUser.Any())
            throw new BusinessException($"The email {request.Email} already exist. If are you Owner, please click on forgot password");

        var roleFilter = new RolesQueryFilter();
        roleFilter.Nombre = "User";
        var findRole = await _roleService.GetListAsync(roleFilter);
        var role = findRole.FirstOrDefault();
        if (role == null)
            throw new BusinessException($"We cannot find a User role, please contact us.");


        var userPassword = "";
        var user = new ApplicationUser
        {
            Email = request.Email,
            NombreCompleto = request.FullName.ToUpper(),
            UserName = request.Email,
            Estado = 1
        };        

        userPassword = _configurationsConstants.PasswordUserExterInitial;
        result = await CreateExternUser(role, user, userPassword, origin);


        return result;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<string> ResetTempCode(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        //generar codigo de 6 cifrans
        var code = new Random().Next(100000, 999999);
        user.TempCode = code.ToString();
        user.ExpireTempCodeDate = DateTime.Now.AddMinutes(10);

        await _userManager.UpdateAsync(user);

        return code.ToString();
    }
}
