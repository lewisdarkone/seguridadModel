using System.Text;
using RazorEngineCore;
using com.softpine.muvany.core.Interfaces;

namespace com.softpine.muvany.infrastructure.SharedServices.Mailing;

/// <summary>
/// 
/// </summary>
public class EmailTemplateService : IEmailMatricesService
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="templateName"></param>
    /// <param name="mailTemplateModel"></param>
    /// <returns></returns>
    public string GenerateEmailTemplate<T>(string templateName, T mailTemplateModel)
    {
        string template = GetTemplate(templateName);

        IRazorEngine razorEngine = new RazorEngine();
        IRazorEngineCompiledTemplate modifiedTemplate = razorEngine.Compile(template);

        return modifiedTemplate.Run(mailTemplateModel);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="templateName"></param>
    /// <returns></returns>
    public string GetTemplate(string templateName)
    {
        string baseDirectory = Directory.GetCurrentDirectory()+ "\\Email Templates\\";
        //string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        //string tmplFolder = Path.Combine(baseDirectory, "Email Templates");
        string filePath = Path.Combine(baseDirectory, $"{templateName}.cshtml");

        using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var sr = new StreamReader(fs, Encoding.Default);
        string mailText = sr.ReadToEnd();
        sr.Close();

        return mailText;
    }
}
