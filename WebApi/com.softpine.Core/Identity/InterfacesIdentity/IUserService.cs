using System.Security.Claims;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.Tools;
using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.QueryFilters;
using com.softpine.muvany.models.CustomEntities;

namespace com.softpine.muvany.core.Interfaces;

/// <summary>
/// 
/// </summary>
public interface IUserService : ITransientService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PaginationResponse<UserDetailsDto>> SearchAsync(UserListFilter filter, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<bool> ExistsWithNameAsync(string name);
    /// <summary>
    /// Validar un código de valicacion enviado
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="validationCode"></param>
    /// <returns></returns>
    Task<bool> ValidateCodeIsValid(string userId, string validationCode);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="email"></param>
    /// <param name="exceptId"></param>
    /// <returns></returns>
    Task<bool> ExistsWithEmailAsync(string email, string? exceptId = null);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <param name="exceptId"></param>
    /// <param name="email"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    Task<bool> ExistsWithPhoneNumberAsync(string phoneNumber, string? exceptId = null, string email = "", string role = "");
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<PagedList<UserDetailsDto>> GetListAsync(UserQueryFilter filter);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> GetCountAsync(CancellationToken cancellationToken);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<UserDetailsDto> GetUserByIdAsync(string userId);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public Task<UserDetailsDto> GetUserByUserNameAsync(string userName);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<List<UserRoleDto>> GetRolesByUserIdAsync(string userId);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<bool> AssignRolesAsync(string userId, UserRolesRequest request);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<string>> GetPermissionsAsync(string userId, CancellationToken cancellationToken);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="permission"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> HasPermissionAsync(string userId, string permission, CancellationToken cancellationToken = default);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task InvalidatePermissionCacheAsync(string userId, CancellationToken cancellationToken);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task ToggleStatusAsync(ToggleUserStatusRequest request);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="principal"></param>
    /// <returns></returns>
    Task<string> GetOrCreateFromPrincipalAsync(ClaimsPrincipal principal);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="origin"></param>
    /// <returns></returns>
    Task<bool> CreateUserAsync(CreateUserRequest request, string origin);
    /// <summary>
    /// Registrar un usuario nuevo
    /// </summary>
    /// <param name="request"></param>
    /// <param name="origin"></param>
    /// <returns></returns>
    Task<bool> RegisterUserAsync(RegisterUserRequest request, string origin);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="origin"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(UpdateUserRequest request, string? origin = "");
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="code"></param>
    /// <param name="tenant"></param>
    /// <returns></returns>
    Task<bool> ConfirmEmailAsync(string userId, string code, string tenant);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    Task<string> ConfirmPhoneNumberAsync(string userId, string code);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="origin"></param>
    /// <returns></returns>
    Task<bool> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<bool> ResetPasswordAsync(ResetPasswordRequest request);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<bool> ChangePasswordAsync(ChangePasswordRequest model, string userId);
    /// <summary>
    /// Proceso validar si el numero digitado es un o no un numero de telefono
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    Task<bool> IsPhoneNumber(string? number);
    /// <summary>
    /// Proceso para cambiar el estatus de un Usuario
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<bool> ChangeStatusAsync(ToggleUserStatusRequest request);
    /// <summary>
    /// Proceso para validar si el usuario consultado es interno o externo
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<bool> ValidateUserInterAsync(string userId);
    /// <summary>
    /// Proceso para obtener el Sucursal del usuario logeado
    /// </summary>
    /// <returns></returns>
    Task<string> GetUserLoginId();


    /// <summary>
    /// Proceso para obtener el usuario logeado
    /// </summary>
    /// <returns></returns>
    Task<UserDetailsDto> GetUserLoginDetails();
    /// <summary>
    /// Obtener todos los usuarios sin restrinciones
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<IEnumerable<UserDetailsDto>> GetUsersAllAsync(UserQueryFilter filter);
    /// <summary>
    /// Agregar inicio de session fallido al colocar la clave mal. Si pasa el limite, tambien bloquea la cuenta
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<bool> AddFailLoggin(string userId);
    /// <summary>
    /// Otorga un codigo de seguridad temporal
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<string> ResetTempCode(string userId);

}
