using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using com.softpine.muvany.core.Exceptions;
using com.softpine.muvany.infrastructure.Identity.Entities;
using com.softpine.muvany.models.Constants;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.Enumerations;
using com.softpine.muvany.models.DTOS;
using com.softpine.muvany.models.QueryFilters;
using System.ServiceModel.Channels;

namespace com.softpine.muvany.infrastructure.Identity.Services;

internal partial class UserService
{
    /// <summary>
    /// This is used when authenticating with AzureAd.
    /// The local user is retrieved using the objectidentifier claim present in the ClaimsPrincipal.
    /// If no such claim is found, an InternalServerException is thrown.
    /// If no user is found with that ObjectId, a new one is created and populated with the values from the ClaimsPrincipal.
    /// If a role claim is present in the principal, and the user is not yet in that roll, then the user is added to that role.
    /// </summary>
    public async Task<string> GetOrCreateFromPrincipalAsync(ClaimsPrincipal principal)
    {
        string? objectId = principal.GetObjectId();
        if ( string.IsNullOrWhiteSpace(objectId) )
        {
            throw new InternalServerException(ApiConstants.Messages.InvalidId);
        }

        var user = await _userManager.Users.Where(u => u.ObjetoId == objectId).FirstOrDefaultAsync()
            ?? await CreateOrUpdateFromPrincipalAsync(principal);

        if ( principal.FindFirstValue(ClaimTypes.Role) is string role &&
            await _roleManager.RoleExistsAsync(role) &&
            !await _userManager.IsInRoleAsync(user, role) )
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        return user.Id;
    }

    private async Task<ApplicationUser> CreateOrUpdateFromPrincipalAsync(ClaimsPrincipal principal)
    {
        string? email = principal.FindFirstValue(ClaimTypes.Upn);
        string? username = principal.GetDisplayName();
        if ( string.IsNullOrEmpty(email) || string.IsNullOrEmpty(username) )
        {
            throw new InternalServerException(ApiConstants.Messages.UserOrEmailNotValid);
        }

        var user = await _userManager.FindByNameAsync(username);
        if ( user is not null && !string.IsNullOrWhiteSpace(user.ObjetoId) )
        {
            throw new InternalServerException(ApiConstants.Messages.UsernameTaken);
        }

        if ( user is null )
        {
            user = await _userManager.FindByEmailAsync(email);
            if ( user is not null && !string.IsNullOrWhiteSpace(user.ObjetoId) )
            {
                throw new InternalServerException(ApiConstants.Messages.EmailTaken);
            }
        }

        IdentityResult? result;
        if ( user is not null )
        {
            user.ObjetoId = principal.GetObjectId();
            result = await _userManager.UpdateAsync(user);

            await _events.PublishAsync(new ApplicationUserUpdatedEvent(user.Id));
        }
        else
        {
            user = new ApplicationUser
            {
                ObjetoId = principal.GetObjectId(),
                NombreCompleto = principal.FindFirstValue(ClaimTypes.GivenName),
                Email = email,
                NormalizedEmail = email.ToUpperInvariant(),
                UserName = username,
                NormalizedUserName = username.ToUpperInvariant(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Estado = 1
            };
            result = await _userManager.CreateAsync(user);

            await _events.PublishAsync(new ApplicationUserCreatedEvent(user.Id));
        }

        if ( !result.Succeeded )
        {
            throw new InternalServerException(ApiConstants.Messages.ValidationErrors, result.GetErrors());
        }

        return user;
    }



    public async Task<bool> CreateUserAsync(CreateUserRequest request, string origin)
    {
        var result = false;
        var UserLoginId = _currentUser.GetUserId();


        var roleUserLogin = await GetRolesByUserIdAsync(UserLoginId.ToString());
        var roleUser = await _roleService.GetByIdAsync(request.RoleId);
        var userPassword = "";
        var dateExpirePassword = request.ExpirePasswordDate != null ? 
            request.ExpirePasswordDate : DateTime.Now.AddDays((int)_configurationsConstants.DefaultExternalUserDaysExpired);
        var user = new ApplicationUser
        {
            Email = request.Email,
            NombreCompleto = request.FullName.ToUpper(),
            UserName = request.Email,
            PhoneNumber = request.PhoneNumber,
            Estado = 1,
            ExpirePasswordDate = dateExpirePassword
        };

        if (roleUserLogin.FirstOrDefault()!.TypeRol == (int)ParametrosGeneralesEnum.Externo && roleUser.TypeRol != (int)ParametrosGeneralesEnum.Externo)
            throw new BusinessException(ApiConstants.Messages.RoleNotAvailableForUser);

        if (roleUser.TypeRol == (int)ParametrosGeneralesEnum.Externo)
        {
            //si es externo, se debe especificar una fecha de expiracion de la clave
            if (!ExpirePasswordDateIsValid(user.ExpirePasswordDate))
                throw new BusinessException("Debe especificar la fecha de expiracion de la contraseña válida.");
            userPassword = _configurationsConstants.PasswordUserExterInitial;
            result = await CreateExternUser(roleUser, user, userPassword, origin);
        }
        else
        {
            userPassword = _configurationsConstants.PasswordUserInterInitial;

            var userDomain = await _usersDomainService.GetUserDomainByEmailAsync(user.Email);
            user.NombreCompleto = userDomain.NombreCompleto;
            user.IdEmpleado = userDomain.IdEmpleado;
            user.Email = userDomain.Email;

            result = await CreateInternUser(roleUser, user, userPassword, origin);

        }
        


        return result;
    }

    /// <summary>
    /// Validar 
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    private bool ExpirePasswordDateIsValid(DateTime? dateTime)
    {
        var date = DateTime.Now.ToString("dd-MM-yy");

        if (dateTime == null) return false;

        if (dateTime.Value.ToString("dd-MM-yy") == date) return false;

        return true;
    }

    private async Task<bool> CreateInternUser(RoleDto roleUser, ApplicationUser user, string userPassword, string origin)
    {
        var result = await _userManager.CreateAsync(user, userPassword);

        if ( !result.Succeeded )
        {
            throw new InternalServerException(ApiConstants.Messages.ValidationErrors, result.GetErrors());
        }

        await _userManager.AddToRoleAsync(user, roleUser.Name);
        user.EmailConfirmed = true;
        _userManager.UpdateAsync(user);

        if ( _securitySettings.RequireConfirmedAccount && !string.IsNullOrEmpty(user.Email) )
        {
            // send verification email
            //await SendEmail(user, origin);
        }

        return true;
    }
    private async Task<bool> CreateExternUser(RoleDto roleUser, ApplicationUser user, string userPassword, string origin)
    {

        user.EmailConfirmed = false;

        var result = await _userManager.CreateAsync(user, userPassword);

        if (!result.Succeeded)
        {
            throw new InternalServerException(ApiConstants.Messages.ValidationErrors, result.GetErrors());
        }

        await _userManager.AddToRoleAsync(user, roleUser.Name);

        await _db.SaveChangesAsync();
        
        if (_securitySettings.RequireConfirmedAccount && !string.IsNullOrEmpty(user.Email))
        {
            // send verification email
            //await SendEmail(user, origin, false);
            //await SendEmailCode(user, origin, false);
            var newCode = await ResetTempCode(user.Id);
            await _mailService.SendEmailCode(user.Email, newCode, user.NombreCompleto);
        }

        return true;
    }
    
    private async Task SendEmail(ApplicationUser user, string origin, bool isInterno = true)
    {
        string emailVerificationUri = await GetEmailVerificationUriAsync(user, origin, isInterno);
     

        var getTemplate = @System.IO.Directory.GetCurrentDirectory() + "\\Files\\EmailTemplates\\emailMuvany.html";
        var mytemplate = File.ReadAllTextAsync(getTemplate).Result;
        string mensaje = $"<h2>Nombre: {user.NombreCompleto.ToUpper()} </h2><h2>Usuario: {user.Email} </h2>";
        var bodyCompleted = mytemplate.Replace("#TitleDescription", "Activate your account on Muvany").Replace("#msgBody", mensaje).Replace("#RedirectTo", emailVerificationUri).Replace("#ReToText", "Activate my account");
        var BodyTemplate = Regex.Replace(bodyCompleted, System.Environment.NewLine, @"\r\n");
        BodyTemplate = Regex.Replace(BodyTemplate, @"""", @"\""");


        var sent = await _mailService.SendEmailSoftpine(user.Email!, BodyTemplate, "Activate your Account on Muvany");

    }

    private async Task SendEmailCode(ApplicationUser user, string origin, bool isInterno = true)
    {
        string emailVerificationUri = await GetEmailVerificationUriAsync(user, origin, isInterno);


        var getTemplate = @System.IO.Directory.GetCurrentDirectory() + "\\Files\\EmailTemplates\\emailMuvany.html";
        var mytemplate = File.ReadAllTextAsync(getTemplate).Result;
        string mensaje = $"<h2>Nombre: {user.NombreCompleto.ToUpper()} </h2><h2>Usuario: {user.Email} </h2>";
        var bodyCompleted = mytemplate.Replace("#TitleDescription", "Activate your account on Muvany").Replace("#msgBody", mensaje).Replace("#RedirectTo", emailVerificationUri).Replace("#ReToText", "Activate my account");
        var BodyTemplate = Regex.Replace(bodyCompleted, System.Environment.NewLine, @"\r\n");
        BodyTemplate = Regex.Replace(BodyTemplate, @"""", @"\""");


        var sent = await _mailService.SendEmailSoftpine(user.Email!, BodyTemplate, "Activate your Account on Muvany");

    }

    public async Task<bool> UpdateAsync(UpdateUserRequest request, string? origin = "")
    {
        var userLoginId = _currentUser.GetUserId();

        var user = await _userManager.FindByIdAsync(request.Id);

        _ = user ?? throw new NotFoundException(ApiConstants.Messages.UserNotFound);

        if ( !string.IsNullOrEmpty(request.FullName) && !user.NombreCompleto.Equals(request.FullName.Trim(), StringComparison.OrdinalIgnoreCase) )
        {
            user.NombreCompleto = request.FullName.Trim().ToUpper();
        }

        string phoneNumber = await _userManager.GetPhoneNumberAsync(user);
        if ( request.PhoneNumber != phoneNumber )
        {
            await _userManager.SetPhoneNumberAsync(user, request.PhoneNumber);
        }

        //Set TwoFactor
        await _userManager.SetTwoFactorEnabledAsync(user,request.TwoFactorEnable);

        user.TwoFactorEnabled = request.TwoFactorEnable;
        var result = await _userManager.UpdateAsync(user);

        //await _signInManager.RefreshSignInAsync(user);

        await _events.PublishAsync(new ApplicationUserUpdatedEvent(user.Id));

        if ( !result.Succeeded )
        {
            throw new InternalServerException(ApiConstants.Messages.UpdateFailed, result.GetErrors());
        }
        return result.Succeeded;
    }
}
