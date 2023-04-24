using com.softpine.muvany.core.Requests;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.QueryFilters;

namespace com.softpine.muvany.core.Interfaces;

/// <summary>
/// Interface para los procesos relacionados al envio de correos
/// </summary>
public interface IMailService : ITransientService
{    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="EmailTo"></param>
    /// <param name="Mensaje"></param>
    /// <param name="Title"></param>
    /// <param name="RedirectUrl"></param>
    /// <param name="RedirectMensaje"></param>
    /// <param name="Departamento"></param>
    /// <returns></returns>
    Task<bool> SendEmail(string EmailTo, string Mensaje, string Title = "", string RedirectUrl = "", string RedirectMensaje = "");
    /// <summary>
    /// 
    /// </summary>
    /// <param name="parEmail"></param>
    /// <returns></returns>
    Task<bool> MailMessage_SendEmail(ParametrosEnvioEmails parEmail);
    /// <summary>
    /// Envia mensaje de correo de prueba
    /// </summary>
    /// <param name="EmailTo"></param>
    /// <param name="Mensaje"></param>
    /// <param name="Title"></param>
    /// <returns></returns>
    Task<bool> SendEmailSoftpine(string EmailTo, string Mensaje, string Title = "");
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SendAsync(MailRequest request, CancellationToken cancellationToken = default);
    /// <summary>
    /// Enviar código de validación
    /// </summary>
    /// <param name="EmailTo"></param>
    /// <param name="code"></param>
    /// <param name="nombreUsuario"></param>
    /// <returns></returns>
    Task<bool> SendEmailCode(string EmailTo, string code, string nombreUsuario = "Usuario");


}
