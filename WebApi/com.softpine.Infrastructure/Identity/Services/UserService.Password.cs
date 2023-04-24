using System.Text.RegularExpressions;
using System.Web;
using com.softpine.muvany.core.Exceptions;
using com.softpine.muvany.models.Constants;
using com.softpine.muvany.models.QueryFilters;
using com.softpine.muvany.models.Requests;

namespace com.softpine.muvany.infrastructure.Identity.Services;

internal partial class UserService
{
    public async Task<bool> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
    {

        var user = await _userManager.FindByEmailAsync(request.Email.Normalize());
        if (user is null)        
            throw new NotFoundException(ApiConstants.Messages.UserNotFound);

        if (!await _userManager.IsEmailConfirmedAsync(user) )
            throw new UnauthorizedException(ApiConstants.Messages.EmailConfirmed_3);

        //Descomentar para validar antes si el usuario es interno y no permitir cambio de contraseña
        //if (await ValidateUserInterAsync(user.Id))
        //    throw new BusinessException(ApiConstants.Messages.UnauthorizedAccess);

        //si la fecha esta expirada, indicar que se comunique con el administrador
        if (user.ExpirePasswordDate != null && user.ExpirePasswordDate.Value <= DateTime.Now)
        {
            throw new UnauthorizedException(ApiConstants.Messages.ExpiredAccount);
        }

        string code = await _userManager.GeneratePasswordResetTokenAsync(user);

        const string route = "reset-password/";
        var unicodePath = HttpUtility.UrlEncodeUnicode($"{code}").ToString();
        var uniemailPath = HttpUtility.UrlEncodeUnicode($"{user.Email}").ToString();


        var newCode = await ResetTempCode(user.Id);
        await _mailService.SendEmailCode(user.Email, newCode, user.NombreCompleto);
        var response = new ResetPasswordRequest { Email = user.Email, Password = null, CodigoValidacion = code };

        return true;
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordRequest request)
    {        

        if (request.ConfirmPassword != request.Password)
            throw new BusinessException(ApiConstants.Messages.ConfirmPassAndPassNotEquals);

        var user = await _userManager.FindByEmailAsync(request.Email?.Normalize());
        // Don't reveal that the user does not exist
        _ = user ?? throw new InternalServerException(ApiConstants.Messages.NotValidParameter);


        //validar que el código sea correcto
        await ValidateCodeIsValid(user.Id, request.CodigoValidacion);
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var activateToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, request.Password);

        if (result.Succeeded != true)
            throw new InternalServerException(result.Errors.FirstOrDefault().Description);

        user.EmailConfirmed = true;
        var update = await _userManager.UpdateAsync(user);

        return result.Succeeded;
    }

    public async Task<bool> ChangePasswordAsync(ChangePasswordRequest model, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);        

        _ = user ?? throw new NotFoundException(ApiConstants.Messages.UserNotFound);

        var result = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);

        if (!result.Succeeded)
        {
            throw new InternalServerException(ApiConstants.Messages.ChangePasswordFailed, result.GetErrors());
        }
        return result.Succeeded;
    }
}
