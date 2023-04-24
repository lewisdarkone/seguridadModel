using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.core.Requests;
using System.Net.Mail;
using com.softpine.muvany.models.QueryFilters;

namespace com.softpine.muvany.infrastructure.SharedServices.Mailing;

/// <summary>
/// 
/// </summary>
public class SmtpMailService : IMailService
{
    private readonly MailSettings _settings;
    private readonly ILogger<SmtpMailService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _config;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="logger"></param>
    /// <param name="unitOfWork"></param>
    /// <param name="config"></param>
    public SmtpMailService(IOptions<MailSettings> settings, ILogger<SmtpMailService> logger, IUnitOfWork unitOfWork, IConfiguration config)
    {
        _settings = settings.Value;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _config = config;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task SendAsync(MailRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var email = new MimeMessage();

            // From
            email.From.Add(new MailboxAddress(_settings.DisplayName, request.From ?? _settings.From));

            // To
            foreach ( string address in request.To )
                email.To.Add(MailboxAddress.Parse(address));

            // Reply To
            if ( !string.IsNullOrEmpty(request.ReplyTo) )
                email.ReplyTo.Add(new MailboxAddress(request.ReplyToName, request.ReplyTo));

            // Bcc
            if ( request.Bcc != null )
            {
                foreach ( string address in request.Bcc.Where(bccValue => !string.IsNullOrWhiteSpace(bccValue)) )
                    email.Bcc.Add(MailboxAddress.Parse(address.Trim()));
            }

            // Cc
            if ( request.Cc != null )
            {
                foreach ( string? address in request.Cc.Where(ccValue => !string.IsNullOrWhiteSpace(ccValue)) )
                    email.Cc.Add(MailboxAddress.Parse(address.Trim()));
            }

            // Headers
            if ( request.Headers != null )
            {
                foreach ( var header in request.Headers )
                    email.Headers.Add(header.Key, header.Value);
            }

            // Content
            var builder = new BodyBuilder();
            email.Sender = new MailboxAddress(request.DisplayName ?? _settings.DisplayName, request.From ?? _settings.From);
            email.Subject = request.Subject;
            builder.HtmlBody = request.Body;

            // Create the file attachments for this e-mail message
            if ( request.AttachmentData != null )
            {
                foreach ( var attachmentInfo in request.AttachmentData )
                    builder.Attachments.Add(attachmentInfo.Key, attachmentInfo.Value);
            }

            email.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls, cancellationToken);
            await smtp.AuthenticateAsync(_settings.UserName, _settings.Password, cancellationToken);
            await smtp.SendAsync(email, cancellationToken);
            await smtp.DisconnectAsync(true, cancellationToken);
        }
        catch ( Exception ex )
        {
            _logger.LogError(ex, ex.Message);
        }
    }

    /// <summary>
    /// Método para envía de correos.
    /// </summary>
    /// <param name="parEmail">Clase con todas las propiedades necesarias para enviar el correo</param>

    public async Task<bool> MailMessage_SendEmail(ParametrosEnvioEmails parEmail)
    {
        bool IsSend = false;

        try
        {
            string urlMiddleware = _config.GetSection($"MailSettings:MiddlewareCommon").Value;
            HttpWebResponse response = null;
            HttpWebRequest request = WebRequest.Create(urlMiddleware) as HttpWebRequest;
            request.Method = "POST";
            request.Headers.Add("Content-Type", "application/json");
            request.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;))");
            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("Host", "staging-obm-api.ofimatic.net");
            request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            request.Headers.Add("Connection", "keep-alive");

            string BobyString = @"
                                {
                                    ""emailTitle"":""" + parEmail.TituloCorreo + @""",
                                    ""emailBody"":""" + parEmail.EmailMensaje + @""",
                                    ""emailReceptor"":""" + parEmail.EmailSender + @""",
                                    ""fromTitle"":""" + "Sistema de Template" + @"""
                                }
                                ";
            //var bbb = $"<div><h1>Confirmación creación de usuario</h1><h2>Nombre: #nombre </h2><h2>Usuario: #email </h2> <a href= #url > Redirigir al portal de Template</a></div>";

            byte[] data = Encoding.UTF8.GetBytes(BobyString);
            using ( var requestStream = await request.GetRequestStreamAsync() )
            {
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();
            }

            using ( response = await request.GetResponseAsync() as HttpWebResponse )
            {

                if ( response.StatusCode == HttpStatusCode.OK )
                {
                    return true;
                }
            }

        }
        catch ( Exception e )
        {

            string errorMsg = e.ToString();
        }

        return IsSend;
    }

    /// <summary>
    /// Funcionalidad para envio de correos
    /// </summary>
    /// <param name="EmailTo"></param>
    /// <param name="Mensaje"></param>
    /// <param name="Title"></param>
    /// <param name="RedirectUrl"></param>
    /// <param name="RedirectMensaje"></param>
    /// <param name="Departamento"></param>
    /// <returns></returns>
    public async Task<bool> SendEmail(string EmailTo, string Mensaje, string Title = "", string RedirectUrl = "", string RedirectMensaje = "")
    {
        var getTemplate = @System.IO.Directory.GetCurrentDirectory() + "\\Files\\EmailTemplates\\emailMuvany.html";
        var mytemplate = File.ReadAllTextAsync(getTemplate).Result;
        var bodyCompleted = mytemplate.Replace("#TitleDescription", Title).Replace("#msgBody", Mensaje).Replace("#EmailSubject", Title).Replace("#RedirectTo", RedirectUrl).Replace("#ReToText", RedirectMensaje);
        var BodyTemplate = Regex.Replace(bodyCompleted, System.Environment.NewLine, @"\r\n");
        BodyTemplate = Regex.Replace(BodyTemplate, @"""", @"\""");

        //var parametrosEmail = new ParametrosEnvioEmails
        //{
        //    EmailSender = EmailTo,
        //    EmailMensaje = BodyTemplate,
        //    TituloCorreo = Title
        //};

        //var result = MailMessage_SendEmail(parametrosEmail).Result;
        var result = SendEmailSoftpine(EmailTo,BodyTemplate,"Activate your Account on Muvany").Result;


        return result;
    }
    /// <summary>
    /// Enviar codigo de validación
    /// </summary>
    /// <param name="EmailTo"></param>
    /// <param name="Mensaje"></param>
    /// <param name="Title"></param>
    /// <param name="RedirectUrl"></param>
    /// <param name="RedirectMensaje"></param>
    /// <returns></returns>
    public async Task<bool> SendEmailCode(string EmailTo, string code, string nombreUsuario = "Usuario")
    {
        var getTemplate = @System.IO.Directory.GetCurrentDirectory() + "\\Files\\EmailTemplates\\emailConfirmacionCode.html";
        var mytemplate = File.ReadAllTextAsync(getTemplate).Result;
        var bodyCompleted = mytemplate.Replace("#nombreUsuario", nombreUsuario).Replace("#codigo", code);

        var result = SendEmailSoftpine(EmailTo, bodyCompleted, "Código de validación").Result;


        return result;
    }
    //TODO: pendient cambiar correo de validacion a codigo de 6 numeros
    /// <summary>
    /// 
    /// </summary>
    /// <param name="EmailTo"></param>
    /// <param name="Mensaje"></param>
    /// <param name="Title"></param>
    /// <returns></returns>
    public async Task<bool> SendEmailSoftpine(string EmailTo, string Mensaje, string Title = "")
    {
        
        using (var client = new SmtpClient(_settings.Host,_settings.Port))
        {
            MailMessage mail = new MailMessage(_settings.From,EmailTo);
            mail.IsBodyHtml = true;
            mail.Body = Mensaje;
            mail.Subject = Title;
            mail.BodyEncoding = Encoding.UTF8;
            client.Credentials = new NetworkCredential(_settings.UserName, _settings.Password);
            client.EnableSsl = true;
            
          await  client.SendMailAsync(mail);
        }
        return true;
    }

}
