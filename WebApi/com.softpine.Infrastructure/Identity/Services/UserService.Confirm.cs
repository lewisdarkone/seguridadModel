using System.Data.Entity;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.WebUtilities;
using com.softpine.muvany.core.Exceptions;
using com.softpine.muvany.infrastructure.Identity.Entities;
using com.softpine.muvany.models.Constants;
using com.softpine.muvany.models.QueryFilters;

namespace com.softpine.muvany.infrastructure.Identity.Services;

internal partial class UserService
{
    private async Task<string> GetEmailVerificationUriAsync(ApplicationUser user, string origin, bool isInterno = true)
    {
        if ( isInterno == true )
        {
            string codeI = await _userManager.GeneratePasswordResetTokenAsync(user);
            codeI = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(codeI));
            var endpointUriI = new Uri(string.Concat($"{origin}"));
            return endpointUriI.ToString();
        }
        string code = await _userManager.GeneratePasswordResetTokenAsync(user);

        const string route = "activate-user/";
        var unicodePath = HttpUtility.UrlEncodeUnicode($"{code}").ToString();
        var uniemailPath = HttpUtility.UrlEncodeUnicode($"{user.Email}").ToString();
        string verificationUri = new Uri(string.Concat($"{origin}", route, $"{unicodePath}/{uniemailPath}/")).ToString();
        return verificationUri;
    }

    public async Task<bool> ConfirmEmailAsync(string userId, string code, string tenant)
    {

        var user = await _userManager.Users
            .Where(u => u.Id == userId && !u.EmailConfirmed)
            .FirstOrDefaultAsync();

        if ( user == null )
            return false;
        _ = user ?? throw new InternalServerException(ApiConstants.Messages.EmailConfirmationError);
        if ( string.IsNullOrEmpty(code) )
        {
            code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        var result = await _userManager.ConfirmEmailAsync(user, code);
        var parametrosEmail = new ParametrosEnvioEmails
        {
            EmailSender = user.Email,
            TituloCorreo = "Cuenta de usuario confirmada",
            EmailMensaje = "Confirmación del registro Completada",
            App = "App Template",
        };

        if ( result.Succeeded )
            await _mailService.MailMessage_SendEmail(parametrosEmail);

        return result.Succeeded;
        //? string.Format(ApiConstants.Messages.MobilePhoneConfirmed_1 + "{0}." + ApiConstants.Messages.MobilePhoneConfirmed_2, user.Email)
        //: throw new InternalServerException(ApiConstants.Messages.EmailConfirmationError);
    }

    public async Task<string> ConfirmPhoneNumberAsync(string userId, string code)
    {

        var user = await _userManager.FindByIdAsync(userId);

        _ = user ?? throw new InternalServerException(ApiConstants.Messages.MobilePhoneConfirmationError);

        var result = await _userManager.ChangePhoneNumberAsync(user, user.PhoneNumber, code);

        return result.Succeeded
            ? user.EmailConfirmed
                ? string.Format(ApiConstants.Messages.MobilePhoneConfirmed_1 + "{0}." + ApiConstants.Messages.MobilePhoneConfirmed_2, user.PhoneNumber)
                : string.Format(ApiConstants.Messages.MobilePhoneConfirmed_1 + "{0}." + ApiConstants.Messages.MobilePhoneConfirmed_3, user.PhoneNumber)
            : throw new InternalServerException(ApiConstants.Messages.MobilePhoneConfirmationError);
    }
}
